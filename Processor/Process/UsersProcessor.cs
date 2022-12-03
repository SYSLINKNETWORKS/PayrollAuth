using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor.Process
{
    public class UsersProcessor : IProcessor<UserBaseModel>
    {
        private readonly DataContext _context;
        AbsBusiness _AbsBusiness;

        private SecurityHelper _SecurityHelper = new SecurityHelper();

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // private ProducerConfig _Kafkaconfig;
        public UsersProcessor(App_Data.DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _AbsBusiness = Builder.MakeBusinessClass(Enums.ClassName.User, _context, _roleManager, _userManager);
        }

        public async Task<ApiResponse> ProcessGet(Guid _MenuId, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _Key = _User.Claims.FirstOrDefault(x => x.Type == Enums.Misc.Key.ToString())?.Value.ToString();

            if (_AbsBusiness != null)
            {
                apiResponse = _SecurityHelper.UserMenuPermission(_MenuId, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermission.View_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }

                var response = await _AbsBusiness.GetDataAsync(_User);

                //Get Employees API
                var _EmployeesApiResponse = await _SecurityHelper.GetEmployeesAsync(_Key);
                if (_EmployeesApiResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _EmployeesApiResponse; }
                var _EmployeesViewModel = (List<EmployeesMicroServiceViewModel>)_EmployeesApiResponse.data;


                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (IEnumerable<ApplicationUser>)response.data;
                    var result = new List<UserViewModel>();
                    foreach (var ViewTable in _Table)
                    {
                        string _EmployeeName = "";
                        var _EmployeeTable = (from _Employee in _EmployeesViewModel.Where(x => x.Id == ViewTable.EmployeeId) select new { EmployeeName = _Employee.Name }).FirstOrDefault();
                        if (_EmployeeTable != null) { _EmployeeName = _EmployeeTable.EmployeeName; }

                        //Get Item Category API
                        var _ItemCategoryApiResponse = await _SecurityHelper.GetItemCategoryAsync(_Key);
                        if (_ItemCategoryApiResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _ItemCategoryApiResponse; }
                        var _ItemCategoryViewModel = (List<MSItemCategoryViewModel>)_ItemCategoryApiResponse.data;



                        var _userItemCategoryTable = await _context.UserItemCategories.Where(rec => rec.UserId == ViewTable.Id.ToString()).ToListAsync();
                        List<UserItemCategoryViewModels> UserItemCategoryViewModels = new List<UserItemCategoryViewModels>();
                        string _ItemCategoryName = "", _RoleName = "";

                        //Item Category
                        foreach (var _record in _userItemCategoryTable)
                        {
                            string _ItemCategoryValue = _ItemCategoryViewModel.Where(x => x.Id == _record.ItemCategoryId).FirstOrDefault() != null ? _ItemCategoryViewModel.Where(x => x.Id == _record.ItemCategoryId).FirstOrDefault().Name.Trim() : "";
                            if (!string.IsNullOrEmpty(_ItemCategoryValue))
                            {
                                _ItemCategoryName += _ItemCategoryValue + ",";
                            }
                        }

                        //User Role
                        var _UserRolesList = (from userRoles in _context.UserRoles
                                              join roles in _context.Roles on userRoles.RoleId equals roles.Id
                                              where userRoles.UserId == ViewTable.Id.ToString()
                                              select new
                                              {
                                                  RoleName = roles.Name,

                                              }).ToList();

                        foreach (var _record in _UserRolesList)
                        {

                            _RoleName += _record.RoleName + ",";

                        }

                        result.Add(new UserViewModel
                        {
                            Id = ViewTable.Id,
                            FirstName = ViewTable.FirstName,
                            LastName = ViewTable.LastName,
                            Email = ViewTable.Email,
                            BranchName = ViewTable.Branches.Name,
                            CompanyName = ViewTable.Branches.Companies.Name,
                            Employee = _EmployeeName,
                            ItemCategoryName = _ItemCategoryName,
                            RoleName = _RoleName,
                            Active = ViewTable.Active,
                            NewPermission = _UserMenuPermission.Insert_Permission,
                            UpdatePermission = _UserMenuPermission.Update_Permission,
                            DeletePermission = _UserMenuPermission.Delete_Permission
                        });
                    }
                    response.data = result;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }
        public async Task<ApiResponse> ProcessGetById(Guid _Id, Guid _MenuId, ClaimsPrincipal _User)
        {
            if (_AbsBusiness != null)
            {
                var _Key = _User.Claims.FirstOrDefault(x => x.Type == Enums.Misc.Key.ToString())?.Value.ToString();
                var response = await _AbsBusiness.GetDataByIdAsync(_Id, _User);
                if (Convert.ToInt32(response.statusCode) == 200)
                {
                    var _Table = (ApplicationUser)response.data;
                    //Get Employees API
                    var _EmployeesApiResponse = await _SecurityHelper.GetEmployeesAsync(_Key);
                    if (_EmployeesApiResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _EmployeesApiResponse; }
                    var _EmployeesViewModel = (List<EmployeesMicroServiceViewModel>)_EmployeesApiResponse.data;

                    //Get Item Category API
                    var _ItemCategoryApiResponse = await _SecurityHelper.GetItemCategoryAsync(_Key);
                    if (_ItemCategoryApiResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _ItemCategoryApiResponse; }
                    var _ItemCategoryViewModel = (List<MSItemCategoryViewModel>)_ItemCategoryApiResponse.data;


                    var _UserRolesList = (from userRoles in _context.UserRoles
                                          join roles in _context.Roles on userRoles.RoleId equals roles.Id
                                          where userRoles.UserId == _Id.ToString()
                                          select new UserRolesViewModels
                                          {
                                              RoleName = roles.Name,
                                              UserId = userRoles.UserId,
                                          }).ToList();

                    string _EmployeeName = "";
                    var _EmployeeTable = (from _Employee in _EmployeesViewModel.Where(x => x.Id == _Table.EmployeeId) select new { EmployeeName = _Employee.Name, EmployeeId = _Employee.Id }).FirstOrDefault();
                    if (_EmployeeTable != null) { _EmployeeName = _EmployeeTable.EmployeeName; }

                    var _userItemCategoryTable = await _context.UserItemCategories.Where(rec => rec.UserId == _Id.ToString()).ToListAsync();
                    List<UserItemCategoryViewModels> UserItemCategoryViewModels = new List<UserItemCategoryViewModels>();
                    foreach (var _record in _userItemCategoryTable)
                    {
                        var _ItemCategoryName = _ItemCategoryViewModel.Where(x => x.Id == _record.ItemCategoryId).FirstOrDefault() != null ? _ItemCategoryViewModel.Where(x => x.Id == _record.ItemCategoryId).FirstOrDefault().Name.Trim() : "";
                        UserItemCategoryViewModels.Add(new UserItemCategoryViewModels
                        {
                            ItemCategoryId = _record.ItemCategoryId,
                            ItemCategoryName = _ItemCategoryName,
                        });
                    }


                    var _ViewModel = new UserViewByIdModel
                    {
                        Id = _Table.Id,
                        FirstName = _Table.FirstName,
                        LastName = _Table.LastName,
                        Email = _Table.Email,
                        PhoneNumber = _Table.PhoneNumber,
                        BranchId = _Table.BranchId,
                        BranchName = _Table.Branches.Name,
                        AllBranchCheck = _Table.AllBranchCheck,
                        // PermissionFrom = _Table.PermissionDateFrom,
                        // PermissionTo = _Table.PermissionDateTo,
                        CompanyName = _Table.Branches.Companies.Name,
                        Employee = _EmployeeName,
                        EmployeeId = _Table.EmployeeId,
                        Active = _Table.Active,
                        Ck_RequiredAttandance = _Table.Ck_RequiredAttandance,
                        Ck_OnlineAttandance = _Table.Ck_OnlineAttandance,
                        BackLog = _Table.BackLog,
                        UserRolesViewModels = _UserRolesList,
                        UserItemCategoryViewModels = UserItemCategoryViewModels,
                    };
                    response.data = _ViewModel;
                }
                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPost(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                var _request = (UserAddModel)request;

                apiResponse = _SecurityHelper.UserMenuPermission(_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString() != StatusCodes.Status200OK.ToString()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel)apiResponse.data;

                if (!_UserMenuPermission.Insert_Permission)
                {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                    return apiResponse;
                }
                var _Table = new ApplicationUser
                {
                    FirstName = _request.FirstName,
                    LastName = _request.LastName,
                    Email = _request.Email,
                    Password = _request.Password,
                    EmployeeId = _request.EmployeeId,
                    PhoneNumber = _request.PhoneNumber,
                    BranchId = _request.BranchId,
                    AllBranchCheck = _request.AllBranchCheck,
                    // PermissionDateFrom = _request.PermissionFrom,
                    // PermissionDateTo = _request.PermissionTo,
                    Active = _request.Active,
                    Ck_RequiredAttandance = _request.Ck_RequiredAttandance,
                    Ck_OnlineAttandance = _request.Ck_OnlineAttandance,
                    BackLog = _request.BackLog,

                    //UserName = _request.Email,
                    //Password = _request.Password,
                    //EmailConfirmed = true,
                    //RolesId = _request.RolesId

                };
                apiResponse = await _AbsBusiness.AddAsync(_Table, _User);

                // var _ItemCategory = _request.UserItemCategoryViewModels.ToList();
                // if (_ItemCategory.Count == 0)
                // {
                //     apiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                //     apiResponse.message = "Record not Found";
                //     return apiResponse;
                // }


                if (apiResponse.statusCode == StatusCodes.Status200OK.ToString())
                {
                    var _UserRole = _request.userRolesList.ToList();
                    if (_UserRole.Count() > 0)
                    {
                        var _UserTable = await _userManager.FindByIdAsync(_Table.Id);
                        foreach (var item in _UserRole)
                        {
                            var resultRole = await _userManager.AddToRoleAsync(_UserTable, item.RoleName);
                        }


                    }

                    List<UserItemCategory> _UserItemCategory = new List<UserItemCategory>();
                    var _ItemCategory = _request.UserItemCategoryViewModels.ToList();

                    if (_ItemCategory.Count() > 0)
                    {
                        foreach (var _Record in _ItemCategory)
                        {
                            _UserItemCategory.Add(new UserItemCategory
                            {
                                UserId = _Table.Id.ToString(),
                                ItemCategoryId = _Record.ItemCategoryId,
                            });
                        }
                        await _context.UserItemCategories.AddRangeAsync(_UserItemCategory);
                        _context.SaveChanges();
                    }
                }
                return apiResponse;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPut(object request, ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            if (_AbsBusiness != null)
            {
                var _request = (UserUpdateModel)request;

                var _Table = new ApplicationUser
                {
                    Id = _request.Id,
                    FirstName = _request.FirstName,
                    LastName = _request.LastName,
                    NormalizedEmail = _request.Email.Trim(),
                    NormalizedUserName = _request.FirstName.Trim() + _request.LastName.Trim(),
                    UserName = _request.FirstName.Trim() + _request.LastName.Trim(),
                    Email = _request.Email,
                    PhoneNumber = _request.PhoneNumber,
                    EmployeeId = _request.EmployeeId,
                    BranchId = _request.BranchId,
                    AllBranchCheck = _request.AllBranchCheck,
                    // PermissionDateFrom = _request.PermissionFrom,
                    // PermissionDateTo = _request.PermissionTo,
                    Active = _request.Active,
                    Ck_RequiredAttandance = _request.Ck_RequiredAttandance,
                    Ck_OnlineAttandance = _request.Ck_OnlineAttandance,
                    BackLog = _request.BackLog,

                };

                apiResponse = await _AbsBusiness.UpdateAsync(_Table, _User);

                var _UserTable = await _userManager.FindByIdAsync(_Table.Id);
                var _UserItemCategoryTable = await _context.UserItemCategories.Where(x => x.UserId == _UserTable.Id).ToListAsync();
                if (_UserItemCategoryTable.Count > 0)
                {
                    foreach (var Record in _UserItemCategoryTable)
                    {
                        _context.UserItemCategories.RemoveRange(Record);
                        _context.SaveChanges();

                    }
                }


                var _ItemCategory = _request.UserItemCategoryViewModels.ToList();
                if (_ItemCategory.Count > 0)
                {
                    List<UserItemCategory> _UserItemCategory = new List<UserItemCategory>();
                    foreach (var _Record in _ItemCategory)
                    {
                        _UserItemCategory.Add(new UserItemCategory
                        {
                            UserId = _UserTable.Id,
                            ItemCategoryId = _Record.ItemCategoryId,
                        });
                    }
                    await _context.UserItemCategories.AddRangeAsync(_UserItemCategory);
                    _context.SaveChanges();
                }
                if (apiResponse.statusCode == StatusCodes.Status200OK.ToString())
                {
                    var _UserRole = _request.userRolesList.ToList();
                    // oldRoles = await _userManager.GetRolesAsync(_UserTable)
                    IList<string> _GetUserRole = new List<string>();
                    _GetUserRole = await _userManager.GetRolesAsync(_UserTable);
                    if (_GetUserRole.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(_UserTable, _GetUserRole);
                    }
                    if (_UserRole.Count() > 0)
                    {
                        _UserTable = await _userManager.FindByIdAsync(_Table.Id);
                        foreach (var item in _UserRole)
                        {
                            var resultRole = await _userManager.AddToRoleAsync(_UserTable, item.RoleName);
                        }
                    }
                }
                return apiResponse;

            }
            return null;
        }
        public async Task<ApiResponse> ProcessDelete(object request, ClaimsPrincipal _User)
        {
            if (_AbsBusiness != null)
            {
                var _request = (UserDeleteModel)request;

                Guid _Id = new Guid(_request.Id);
                Guid _MenuId = _request.Menu_Id;
                return await _AbsBusiness.DeleteAsync(_Id, _User);
            }
            return null;
        }

    }
}