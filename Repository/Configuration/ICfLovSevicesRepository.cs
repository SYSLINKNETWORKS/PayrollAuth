using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Repository
{
    public interface ICfLovServicesRepository
    {

        Task<ApiResponse> GetComppaniesLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetBranchesLovAsync(ClaimsPrincipal _User, string _Search);

        Task<ApiResponse> GetFinancialYearsLovAsync(ClaimsPrincipal _User, string _Search);
        //Task<ApiResponse> GetEmployeeLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetUsersLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetUserMenuLovAsync(ClaimsPrincipal _User);
        Task<ApiResponse> GetUserReportMenuLovAsync(ClaimsPrincipal _User, Guid _MenuId);
        Task<ApiResponse> GetRolesLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetModuleLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetCategoriesLovAsync(ClaimsPrincipal _User, string _Search);

        Task<ApiResponse> GetSubCategoryLovAsync(ClaimsPrincipal _User, string _Search);
        Task<ApiResponse> GetMenuPermissionByMenuIdLovAsync(ClaimsPrincipal _User, Guid _MenuId);
        Task<ApiResponse> GetDashboardPermissionByNameLovAsync(ClaimsPrincipal _User, String _MenuName);

    }
    public class CfLovServicesRepository : ICfLovServicesRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private ErrorLog _ErrorLog = new ErrorLog();
        private readonly DataContext _context = null;
        private readonly IConfiguration _configuration;
        string _UserId = "";

        public CfLovServicesRepository(DataContext context, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _configuration = configuration;

        }

        public async Task<ApiResponse> GetComppaniesLovAsync(ClaimsPrincipal _User, string _Search)
        {
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();

            var apiResponse = new ApiResponse();
            try
            {

                var _CfCompanies = await (from CfCompanies in _context.Companies.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search)))
                                          select new LovServicesViewModel
                                          {
                                              Id = CfCompanies.Id,
                                              Name = CfCompanies.Name
                                          }).OrderBy(o => o.Name).ToListAsync();

                if (_CfCompanies == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_CfCompanies.Count == 0)
                {
                    _CfCompanies = await (from CfCompanies in _context.Companies.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true)
                                          select new LovServicesViewModel
                                          {
                                              Id = CfCompanies.Id,
                                              Name = CfCompanies.Name
                                          }).OrderBy(o => o.Name).ToListAsync();
                }
                if (_CfCompanies.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _CfCompanies;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }
        public async Task<ApiResponse> GetBranchesLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var apiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {

                var _CfBranches = await (from CfBranches in _context.Branches.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search)))
                                         select new LovServicesViewModel
                                         {
                                             Id = CfBranches.Id,
                                             Name = CfBranches.Name
                                         }).OrderBy(o => o.Name).ToListAsync();

                if (_CfBranches == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                if (_CfBranches.Count == 0)
                {
                    _CfBranches = await (from CfBranches in _context.Branches.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true)
                                         select new LovServicesViewModel
                                         {
                                             Id = CfBranches.Id,
                                             Name = CfBranches.Name
                                         }).OrderBy(o => o.Name).ToListAsync();

                }

                if (_CfBranches.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _CfBranches;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }
        public async Task<ApiResponse> GetFinancialYearsLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var apiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {
                //                string _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
                List<LovServicesViewModel> _FinancialYears = new List<LovServicesViewModel>();
                var _FinancialYearsActive = await (from FinancialYears in _context.FinancialYears.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true).OrderByDescending(x => x.StartDate)
                                                   select new LovServicesViewModel
                                                   {
                                                       Id = FinancialYears.Id,
                                                       Name = FinancialYears.StartYear.ToString() + "/" + FinancialYears.EndYear.ToString()
                                                   }).ToListAsync();

                var _FinancialYearsInActive = await (from FinancialYears in _context.FinancialYears.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == false).OrderByDescending(x => x.StartDate)
                                                     select new LovServicesViewModel
                                                     {
                                                         Id = FinancialYears.Id,
                                                         Name = FinancialYears.StartYear.ToString() + "/" + FinancialYears.EndYear.ToString()
                                                     }).ToListAsync();

                _FinancialYears.AddRange(_FinancialYearsActive);
                _FinancialYears.AddRange(_FinancialYearsInActive);

                if (_FinancialYears == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_FinancialYears.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _FinancialYears;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;


            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;
            }
        }


        public async Task<ApiResponse> GetUsersLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _Users = await (from User in _context.Users.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.UserName.Contains(_Search)))
                                    select new LovServicesViewModel
                                    {
                                        Id = new Guid(User.Id),
                                        Name = User.FirstName + " " + User.LastName
                                    }).OrderBy(o => o.Name).ToListAsync();

                if (_Users == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                // if (_Users.Count == 0) {
                //     _Users = await (from User in _context.Users.Where (a => a.Action != Enums.Operations.D.ToString () && a.Active == true) select new LovServicesViewModel {
                //             Id = new Guid(User.Id),
                //             Name = User.FirstName + " " + User.LastName
                //     }).OrderBy (o => o.Name).ToListAsync ();
                // }
                if (_Users.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Users;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public async Task<ApiResponse> GetUserMenuLovAsync(ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {

                var _UserKey = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
                var _userInformation = await _context.UserLoginAudits.Where(x => x.Key == _UserKey.ToString()).Include(u => u.ApplicationUsers).Include(b => b.Branches).Include(c => c.Companies).Include(y => y.FinancialYears).FirstOrDefaultAsync();

                GetMenuViewModel _GetMenuViewModel = new GetMenuViewModel();

                _GetMenuViewModel.CompanyName = _userInformation.Companies.Name;
                _GetMenuViewModel.BranchName = _userInformation.Branches.Name;
                _GetMenuViewModel.FiscalYear = _userInformation.FinancialYears.StartYear + "/" + _userInformation.FinancialYears.EndYear;
                _GetMenuViewModel.UserName = _userInformation.ApplicationUsers.UserName;
                _GetMenuViewModel.Version = typeof(ICfLovServicesRepository).Assembly.GetName().Version.ToString(); ;

                var result = await (from _Users in _context.Users
                                    join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                    join _UserRolePermissions in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermissions.Roles_Id
                                    join _UserMenu in _context.UserMenus on _UserRolePermissions.Menu_Id equals _UserMenu.Id
                                    join _UserModule in _context.UserMenuModules on _UserMenu.ModuleId equals _UserModule.Id
                                    join _UserMenuSubCategory in _context.UserMenuSubCategories on _UserMenu.SubCategoryId equals _UserMenuSubCategory.Id
                                    join _UserMenuCategory in _context.UserMenuCategories on _UserMenuSubCategory.CategoryId equals _UserMenuCategory.Id
                                    where _UserMenu.Action != Enums.Operations.D.ToString() && _Users.Id == _userInformation.UserId && _UserMenu.Active == true && _UserMenu.View == true && _UserRolePermissions.View_Permission == true && _UserModule.Active == true && _UserMenuSubCategory.Active == true && _UserMenuCategory.Active == true
                                    select new GetMenuUserWise
                                    {

                                        MenuId = _UserMenu.Id,
                                        MenuName = _UserMenu.Name,
                                        MenuAlias = _UserMenu.Alias,
                                        MenuUrl = _UserModule.Name + "/" + _UserMenuSubCategory.Name + "/" + _UserMenu.Name + "?M=" + _UserMenu.Id,
                                        MenuCategoryId = _UserMenuCategory.Id,
                                        MenuCategoryName = _UserMenuCategory.Name,

                                        MenuSubCategoryId = _UserMenuSubCategory.Id,
                                        MenuSubCategoryName = _UserMenuSubCategory.Name,
                                        MenuSubCategoryIcon = _UserMenuSubCategory.Icon,

                                        ModuleId = _UserModule.Id,
                                        ModuleName = _UserModule.Name,
                                        ModuleIcon = _UserModule.Icon

                                    }).ToListAsync();

                _GetMenuViewModel.GetMenuUserWises = result;

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _GetMenuViewModel;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }

        public async Task<ApiResponse> GetUserReportMenuLovAsync(ClaimsPrincipal _User, Guid _MenuId)
        {
            var apiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {

                var _UserKey = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
                var _userInformation = await _context.UserLoginAudits.Where(x => x.Key == _UserKey.ToString()).Include(u => u.ApplicationUsers).Include(b => b.Branches).Include(c => c.Companies).Include(y => y.FinancialYears).FirstOrDefaultAsync();
                var _ModuleId = await _context.UserMenus.Where(m => m.Id == _MenuId).FirstOrDefaultAsync();

                var _GetMenuReportUserWise = await (from _Users in _context.Users
                                                    join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                                    join _UserRolePermissions in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermissions.IdentityRoles.Id
                                                    join _UserMenu in _context.UserMenus on _UserRolePermissions.Menu_Id equals _UserMenu.Id
                                                    join _UserModule in _context.UserMenuModules on _UserMenu.ModuleId equals _UserModule.Id
                                                    join _UserMenuSubCategory in _context.UserMenuSubCategories on _UserMenu.SubCategoryId equals _UserMenuSubCategory.Id
                                                    join _UserMenuCategory in _context.UserMenuCategories on _UserMenuSubCategory.CategoryId equals _UserMenuCategory.Id
                                                    where _UserMenu.Action != Enums.Operations.D.ToString() && _Users.Id == _userInformation.UserId && _UserMenu.Active == true && _UserRolePermissions.Print_Permission == true && _UserModule.Active == true && _UserMenuSubCategory.Active == true && _UserMenuCategory.Active == true && _UserMenu.ModuleId == _ModuleId.ModuleId && _UserMenuSubCategory.Name == "Report"
                                                    select new GetMenuReportUserWise
                                                    {

                                                        MenuId = _UserMenu.Id,
                                                        MenuName = _UserMenu.Name,
                                                        MenuAlias = _UserMenu.Alias,

                                                    }).ToListAsync();

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _GetMenuReportUserWise;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }

        public async Task<ApiResponse> GetUserMenuModuleLovAsync(ClaimsPrincipal _User)
        {
            var apiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {

                var _UserKey = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
                var _userInformation = await _context.UserLoginAudits.Where(x => x.Key == _UserKey.ToString()).Include(u => u.ApplicationUsers).Include(b => b.Branches).Include(c => c.Companies).Include(y => y.FinancialYears).FirstOrDefaultAsync();

                GetMenuViewModel _GetMenuViewModel = new GetMenuViewModel();

                _GetMenuViewModel.CompanyName = _userInformation.Companies.Name;
                _GetMenuViewModel.BranchName = _userInformation.Branches.Name;
                _GetMenuViewModel.FiscalYear = _userInformation.FinancialYears.StartYear + "/" + _userInformation.FinancialYears.EndYear;
                _GetMenuViewModel.UserName = _userInformation.ApplicationUsers.UserName;
                _GetMenuViewModel.Version = typeof(ICfLovServicesRepository).Assembly.GetName().Version.ToString();

                var result = await (from _Users in _context.Users
                                    join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                    join _UserRolePermissions in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermissions.IdentityRoles.Id
                                    join _UserMenu in _context.UserMenus on _UserRolePermissions.Menu_Id equals _UserMenu.Id
                                    join _UserModule in _context.UserMenuModules on _UserMenu.ModuleId equals _UserModule.Id
                                    join _UserMenuSubCategory in _context.UserMenuSubCategories on _UserMenu.SubCategoryId equals _UserMenuSubCategory.Id
                                    join _UserMenuCategory in _context.UserMenuCategories on _UserMenuSubCategory.CategoryId equals _UserMenuCategory.Id
                                    where _Users.Id == _userInformation.UserId && _UserMenu.Active == true && _UserMenu.View == true && _UserModule.Active == true && _UserMenuSubCategory.Active == true && _UserMenuCategory.Active == true
                                    select new GetMenuUserWise
                                    {

                                        MenuId = _UserMenu.Id,
                                        MenuName = _UserMenu.Name,
                                        MenuAlias = _UserMenu.Alias,
                                        MenuUrl = _UserModule.Name + "/" + _UserMenuSubCategory.Name + "/" + _UserMenu.Name + "?M=" + _UserMenu.Id,
                                        MenuCategoryId = _UserMenuCategory.Id,
                                        MenuCategoryName = _UserMenuCategory.Name,

                                        MenuSubCategoryId = _UserMenuSubCategory.Id,
                                        MenuSubCategoryName = _UserMenuSubCategory.Name,
                                        MenuSubCategoryIcon = _UserMenuSubCategory.Icon,

                                        ModuleId = _UserModule.Id,
                                        ModuleName = _UserModule.Name,
                                        ModuleIcon = _UserModule.Icon

                                    }).ToListAsync();

                _GetMenuViewModel.GetMenuUserWises = result;

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _GetMenuViewModel;
                return apiResponse;

            }
            catch (DbUpdateException e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }
        public async Task<ApiResponse> GetModuleLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var apiResponse = new ApiResponse();
            try
            {

                var _UserMenuModules = await (from UserMenuModules in _context.UserMenuModules.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search)))
                                              select new LovServicesViewModel
                                              {
                                                  Id = UserMenuModules.Id,
                                                  Name = UserMenuModules.Name
                                              }).OrderBy(o => o.Name).ToListAsync();

                if (_UserMenuModules == null)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }
                if (_UserMenuModules.Count == 0)
                {
                    _UserMenuModules = await (from UserMenuModules in _context.UserMenuModules.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true)
                                              select new LovServicesViewModel
                                              {
                                                  Id = UserMenuModules.Id,
                                                  Name = UserMenuModules.Name
                                              }).OrderBy(o => o.Name).ToListAsync();
                }
                if (_UserMenuModules.Count == 0)
                {
                    apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    apiResponse.message = "Record not found";
                    return apiResponse;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.data = _UserMenuModules;
                return apiResponse;

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                apiResponse.message = _ErrorId;
                return apiResponse;

            }
        }

        //Get Categories
        public async Task<ApiResponse> GetCategoriesLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _UserMenuCategories = await (from UserMenuCategories in _context.UserMenuCategories.Where(a => a.Action != Enums.Operations.D.ToString() && (string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search)))
                                                 select new LovServicesViewModel
                                                 {
                                                     Id = UserMenuCategories.Id,
                                                     Name = UserMenuCategories.Name
                                                 }).OrderBy(o => o.Name).ToListAsync();

                if (_UserMenuCategories == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                if (_UserMenuCategories.Count == 0)
                {
                    _UserMenuCategories = await (from UserMenuCategories in _context.UserMenuCategories.Where(a => a.Action != Enums.Operations.D.ToString())
                                                 select new LovServicesViewModel
                                                 {
                                                     Id = UserMenuCategories.Id,
                                                     Name = UserMenuCategories.Name
                                                 }).OrderBy(o => o.Name).ToListAsync();
                }
                if (_UserMenuCategories.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _UserMenuCategories;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public async Task<ApiResponse> GetSubCategoryLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _UserMenuSubCategories = await (from UserMenuSubCategories in _context.UserMenuSubCategories.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true && (string.IsNullOrEmpty(_Search) ? true : a.Name.Contains(_Search)))
                                                    select new LovServicesViewModel
                                                    {
                                                        Id = UserMenuSubCategories.Id,
                                                        Name = UserMenuSubCategories.Name
                                                    }).OrderBy(o => o.Name).ToListAsync();

                if (_UserMenuSubCategories == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                if (_UserMenuSubCategories.Count == 0)
                {
                    _UserMenuSubCategories = await (from UserMenuSubCategories in _context.UserMenuSubCategories.Where(a => a.Action != Enums.Operations.D.ToString() && a.Active == true)
                                                    select new LovServicesViewModel
                                                    {
                                                        Id = UserMenuSubCategories.Id,
                                                        Name = UserMenuSubCategories.Name
                                                    }).OrderBy(o => o.Name).ToListAsync();
                }
                if (_UserMenuSubCategories.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _UserMenuSubCategories;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }

        public async Task<ApiResponse> GetRolesLovAsync(ClaimsPrincipal _User, string _Search)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _UserRole = await (from Roles in _context.Roles
                                       where (string.IsNullOrEmpty(_Search) ? true : Roles.Name.Contains(_Search))
                                       select new LovServicesViewModel
                                       {
                                           Id = Guid.Parse(Roles.Id),
                                           Name = Roles.Name
                                       }).OrderBy(o => o.Name).ToListAsync();

                if (_UserRole == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                if (_UserRole.Count == 0)
                {
                    _UserRole = await (from Roles in _context.Roles
                                       select new LovServicesViewModel
                                       {
                                           Id = Guid.Parse(Roles.Id),
                                           Name = Roles.Name
                                       }).OrderBy(o => o.Name).ToListAsync();
                }
                if (_UserRole.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _UserRole;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public async Task<ApiResponse> GetMenuPermissionByMenuIdLovAsync(ClaimsPrincipal _User, Guid _MenuId)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();


                var _Permission = await (from _UserKey in _context.UserLoginAudits
                                         join _Users in _context.Users on _UserKey.UserId equals _Users.Id
                                         join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                         join _UserRolePermission in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermission.Roles_Id
                                         join _UserMenu in _context.UserMenus on _UserRolePermission.Menu_Id equals _UserMenu.Id
                                         join _Company in _context.Companies on _UserKey.CompanyId equals _Company.Id
                                         join _Branch in _context.Branches on _UserKey.BranchId equals _Branch.Id
                                         join _Year in _context.FinancialYears on _UserKey.YearId equals _Year.Id
                                         where _Users.Id == _UserId.ToString() && _UserMenu.Id == _MenuId
                                         select new UserPermissionViewModel
                                         {
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

                                         }).FirstOrDefaultAsync();

                if (_Permission == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Permission;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }



        public async Task<ApiResponse> GetDashboardPermissionByNameLovAsync(ClaimsPrincipal _User, String _MenuName)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();


                var _Permission = await (from _UserKey in _context.UserLoginAudits
                                         join _Users in _context.Users on _UserKey.UserId equals _Users.Id
                                         join _UserRoles in _context.UserRoles on _Users.Id equals _UserRoles.UserId
                                         join _UserRolePermission in _context.UserRolePermissions on _UserRoles.RoleId equals _UserRolePermission.Roles_Id
                                         join _UserMenu in _context.UserMenus on _UserRolePermission.Menu_Id equals _UserMenu.Id
                                         join _Company in _context.Companies on _UserKey.CompanyId equals _Company.Id
                                         join _Branch in _context.Branches on _UserKey.BranchId equals _Branch.Id
                                         join _Year in _context.FinancialYears on _UserKey.YearId equals _Year.Id
                                         where _Users.Id == _UserId.ToString() && _UserMenu.Name.ToUpper() == _MenuName.ToUpper()
                                         select new UserPermissionViewModel
                                         {
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

                                         }).ToArrayAsync();//.FirstOrDefaultAsync();

                if (_Permission == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Permission;
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
    }
}