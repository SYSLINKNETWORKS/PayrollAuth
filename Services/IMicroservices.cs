using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Services
{
    public interface IMicroservices
    {

        Task<ApiResponse> UserKeyAsync(ClaimsPrincipal _User);
        Task<ApiResponse> UserKeyVerificationAsync(string _Key);
        Task<ApiResponse> UserLoginInfoAsync(string _Key);
        Task<ApiResponse> MenuInfoAsync(String _Key);

        Task<ApiResponse> UserMenuPermissionAsync(Guid _MenuId, String _Key);
        Task<ApiResponse> BranchInfoAsync(String _Key);
        Task<ApiResponse> FinancialYearAsync(String _Key);


    }

    public class Microservices : IMicroservices
    {

        private readonly DataContext _context;
        SecurityHelper _SecurityHelper = new SecurityHelper();

        public Microservices(DataContext context)
        {
            _context = context;
        }



        public async Task<ApiResponse> UserKeyAsync(ClaimsPrincipal _User)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _Key = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
            var _UserKey = await _context.UserLoginAudits.Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserKey == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            return apiResponse;
        }

        public async Task<ApiResponse> UserKeyVerificationAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _UserKey = await _context.UserLoginAudits.Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserKey == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }
            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            return apiResponse;
        }

        public async Task<ApiResponse> UserLoginInfoAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();
            var _UserTable = await _context.UserLoginAudits.Include(u => u.ApplicationUsers).Include(c => c.Companies).Include(b => b.Branches).Include(y => y.FinancialYears).Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserTable == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }
            var _UserItemCategory = await _context.UserItemCategories.Where(x => x.UserId == _UserTable.ApplicationUsers.Id.ToString()).ToListAsync();

            List<string> _ItemCategoryIdList = new List<string>();


            UserLoginInfoViewModel _UserLoginInfoViewModel = new UserLoginInfoViewModel();


            int _rowcnt = 0;
            foreach (var _Record in _UserItemCategory)
            {

                _ItemCategoryIdList.Insert(_rowcnt, _Record.ItemCategoryId.ToString());
                _rowcnt += 1;

            }

            _UserLoginInfoViewModel.CompanyId = _UserTable.CompanyId;
            _UserLoginInfoViewModel.CompanyName = _UserTable.Companies.Name.Trim();
            _UserLoginInfoViewModel.BranchId = _UserTable.BranchId;
            _UserLoginInfoViewModel.BranchName = _UserTable.Branches.Name;
            _UserLoginInfoViewModel.UserId = _UserTable.UserId;
            _UserLoginInfoViewModel.UserFirstName = _UserTable.ApplicationUsers.FirstName.Trim();
            _UserLoginInfoViewModel.UserLastName = _UserTable.ApplicationUsers.LastName.Trim();
            _UserLoginInfoViewModel.UserName = _UserTable.ApplicationUsers.FirstName.Trim() + " " + _UserTable.ApplicationUsers.LastName.Trim();
            _UserLoginInfoViewModel.UserEmail = _UserTable.ApplicationUsers.Email;
            _UserLoginInfoViewModel.UserMobile = _UserTable.ApplicationUsers.PhoneNumber;
            _UserLoginInfoViewModel.FinancialYearId = _UserTable.FinancialYears.Id;
            _UserLoginInfoViewModel.FinancialYearName = _UserTable.FinancialYears.StartYear.ToString().Trim() + "/" + _UserTable.FinancialYears.EndYear.ToString().Trim();
            _UserLoginInfoViewModel.DateStart = _UserTable.FinancialYears.StartDate;
            _UserLoginInfoViewModel.DateEnd = _UserTable.FinancialYears.EndDate;
            _UserLoginInfoViewModel.ckSalesman = _UserTable.CkSalesman;
            _UserLoginInfoViewModel.ckDirector = _UserTable.CkDirector;
            _UserLoginInfoViewModel.EmployeeId = _UserTable.EmployeeId;
            _UserLoginInfoViewModel.ItemCategoryIdList = _ItemCategoryIdList;


            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _UserLoginInfoViewModel;
            return apiResponse;
        }

        public async Task<ApiResponse> UserMenuPermissionAsync(Guid _MenuId, string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();


            var _Permission = await (from _UserKey in _context.UserLoginAudits
                                     join _Users in _context.Users on _UserKey.UserId equals _Users.Id
                                     join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                     join _UserRolePermission in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermission.Roles_Id
                                     join _UserMenu in _context.UserMenus on _UserRolePermission.Menu_Id equals _UserMenu.Id
                                     join _Company in _context.Companies on _UserKey.CompanyId equals _Company.Id
                                     join _Branch in _context.Branches on _UserKey.BranchId equals _Branch.Id
                                     join _Year in _context.FinancialYears on _UserKey.YearId equals _Year.Id
                                     where _UserKey.Key == _Key && _UserRolePermission.Menu_Id == _MenuId
                                     && (_UserRolePermission.View_Permission == true || _UserRolePermission.Insert_Permission == true || _UserRolePermission.Update_Permission == true || _UserRolePermission.Delete_Permission == true || _UserRolePermission.Print_Permission == true || _UserRolePermission.Check_Permission == true || _UserRolePermission.Approve_Permission == true)
                                     select new UserPermissionViewModel
                                     {
                                         UserId = _Users.Id,
                                         UserName = _Users.FirstName.Trim() + " " + _Users.LastName.Trim(),
                                         MenuId = _UserRolePermission.Menu_Id,
                                         MenuName = _UserMenu.Name,
                                         MenuAlias = _UserMenu.Alias,
                                         CompanyId = _UserKey.CompanyId,
                                         CompanyName = _Company.Name,
                                         BranchId = _UserKey.BranchId,
                                         BranchName = _Branch.Name,
                                         FinancialYearId = _UserKey.YearId.Value,
                                         FinancialYearName = _Year.StartYear.ToString() + "/" + _Year.EndYear.ToString(),
                                         View_Permission = _UserRolePermission.View_Permission,
                                         Insert_Permission = _UserRolePermission.Insert_Permission,
                                         Update_Permission = _UserRolePermission.Update_Permission,
                                         Delete_Permission = _UserRolePermission.Delete_Permission,
                                         Print_Permission = _UserRolePermission.Print_Permission,
                                         Check_Permission = _UserRolePermission.Check_Permission,
                                         Approve_Permission = _UserRolePermission.Approve_Permission,
                                         YearStartDate = _Year.StartDate,
                                         YearEndDate = _Year.EndDate,
                                         PermissionDateFrom = DateTime.Today.AddDays(-_Users.BackLog),// _Users.PermissionDateFrom,
                                         PermissionDateTo = DateTime.Today.AddDays(365),
                                         ckSalesman = _UserKey.CkSalesman,
                                         ckDirector = _UserKey.CkDirector,
                                         EmployeeId = _Users.EmployeeId,

                                     }).FirstOrDefaultAsync();
            if (_Permission == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Invalid Permission";
                return apiResponse;
            }

            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _Permission;
            return apiResponse;

        }

        public async Task<ApiResponse> MenuInfoAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var _UserKey = await _context.UserLoginAudits.Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserKey == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Invalid Key";
                return apiResponse;
            }



            var _MenuTable = await (from _UserMenu in _context.UserMenus
                                    join _UserMenuModule in _context.UserMenuModules on _UserMenu.ModuleId equals _UserMenuModule.Id
                                    join _UserMenuSubCategory in _context.UserMenuSubCategories on _UserMenu.SubCategoryId equals _UserMenuSubCategory.Id
                                    join _UserMenuSubMasterCategory in _context.UserMenuCategories on _UserMenuSubCategory.CategoryId equals _UserMenuSubMasterCategory.Id
                                    where _UserMenu.Action != Enums.Operations.D.ToString()
                                    select new MenuInfoViewModel
                                    {
                                        MenuId = _UserMenu.Id,
                                        MenuName = _UserMenu.Name,
                                        MenuAlias = _UserMenu.Alias,
                                        ModuleId = _UserMenuModule.Id,
                                        ModuleName = _UserMenuModule.Name,
                                        SubCategoryId = _UserMenu.SubCategoryId,
                                        SubCategoryName = _UserMenuSubCategory.Name,
                                        SubCategoryMasterId = _UserMenuSubCategory.CategoryId,
                                        SubCategoryMasterName = _UserMenuSubMasterCategory.Name,
                                    }
            ).ToListAsync();


            if (_MenuTable.Count == 0)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Record not found";
                return apiResponse;
            }

            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _MenuTable;
            return apiResponse;

        }
        public async Task<ApiResponse> BranchInfoAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var _UserInfo = await _context.UserLoginAudits.Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserInfo == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString(); // "401";
                apiResponse.message = "Invalid Key";
                return apiResponse;

            }
            var _Table = await _context.Branches.Include(c => c.Companies).Where(a => a.Action != Enums.Operations.D.ToString() && a.CompanyId == _UserInfo.CompanyId).ToListAsync();

            if (_Table == null)
            {
                apiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                apiResponse.message = "Record Not Found";
                return apiResponse;
            }

            if (_Table.Count == 0)
            {
                apiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); // "401";
                apiResponse.message = "Record Not Found";
                return apiResponse;
            }

            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings_Image.json", false)
              .Build();

            String _UrlNotification = configuration["ImageLogoUrl"];

            List<BranchInfoViewModel> _BranchInfoViewModel = new List<BranchInfoViewModel>();


            foreach (var _Record in _Table)
            {
                _BranchInfoViewModel.Add(new BranchInfoViewModel
                {

                    BranchId = _Record.Id,
                    BranchName = _Record.Name,
                    ShortName = _Record.ShortName,
                    Address = string.IsNullOrEmpty(_Record.Address) ? "" : _Record.Address,
                    Phone = string.IsNullOrEmpty(_Record.Phone) ? "" : _Record.Phone,
                    Mobile = string.IsNullOrEmpty(_Record.Mobile) ? "" : _Record.Mobile,
                    Email = string.IsNullOrEmpty(_Record.Email) ? "" : _Record.Email,
                    Web = string.IsNullOrEmpty(_Record.Web) ? "" : _Record.Web,
                    Fax = string.IsNullOrEmpty(_Record.Fax) ? "" : _Record.Fax,
                    NTN = string.IsNullOrEmpty(_Record.Companies.NTN) ? "" : _Record.Companies.NTN,
                    STN = string.IsNullOrEmpty(_Record.Companies.STN) ? "" : _Record.Companies.STN,
                    ImageUrlHeader = string.IsNullOrEmpty(_Record.LogoHeader) ? "" : _UrlNotification + "//" + _Record.LogoHeader,
                    ImageUrlFooter = string.IsNullOrEmpty(_Record.LogoFooter) ? "" : _UrlNotification + "//" + _Record.LogoFooter,
                    HeadOffice = _Record.HeadOffice,
                    Type = _Record.Type,
                    CompanyId = _Record.CompanyId,
                    CompanyName = _Record.Companies.Name,
                    Active = _Record.Active
                });

            }

            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _BranchInfoViewModel;
            return apiResponse;

        }

        public async Task<ApiResponse> FinancialYearAsync(string _Key)
        {
            ApiResponse apiResponse = new ApiResponse();

            var _UserInfo = await _context.UserLoginAudits.Where(x => x.Key == _Key && x.Status == true).FirstOrDefaultAsync();
            if (_UserInfo == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString(); // "401";
                apiResponse.message = "Invalid Key";
                return apiResponse;

            }
            var _Table = await _context.FinancialYears.Where(a => a.Type != true.ToString() && a.Action != Enums.Operations.D.ToString()).FirstOrDefaultAsync();

            if (_Table == null)
            {
                apiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                apiResponse.message = "Record Not Found";
                return apiResponse;
            }

            MSFinancialYearViewModel _MSFinancialYearViewModel = new MSFinancialYearViewModel();
            _MSFinancialYearViewModel.Id = _Table.Id;
            _MSFinancialYearViewModel.StartDate = _Table.StartDate;
            _MSFinancialYearViewModel.EndDate = _Table.EndDate;


            apiResponse.statusCode = StatusCodes.Status200OK.ToString();
            apiResponse.data = _MSFinancialYearViewModel;
            return apiResponse;

        }



    }
}