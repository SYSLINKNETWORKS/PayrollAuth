using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor {
    public class GetMenuProcessor : IProcessor<GetMenuBaseModel> {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        //string _UserId = "";

        public GetMenuProcessor (App_Data.DataContext context) {
            _context = context;

            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.GetMenu, _context);
        }
        public async Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                // (await _context.Users.Where (s => s.Id == _UserId).Include (b => b.Branches).FirstOrDefaultAsync ()).Branches.CompanyId
                var _UserKey = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.Key.ToString ())?.Value.ToString ();
                var _userInformation = await _context.UserLoginAudits.Where (x => x.Key == _UserKey.ToString ()).Include (u => u.ApplicationUsers).Include (b => b.Branches).Include (c => c.Companies).Include (y => y.FinancialYears).FirstOrDefaultAsync ();
                GetMenuViewModel _GetMenuViewModel = new GetMenuViewModel ();

                _GetMenuViewModel.CompanyName = _userInformation.Companies.Name;
                _GetMenuViewModel.BranchName = _userInformation.Branches.Name;
                _GetMenuViewModel.FiscalYear = _userInformation.FinancialYears.StartYear + "/" + _userInformation.FinancialYears.EndYear;
                _GetMenuViewModel.UserName = _userInformation.ApplicationUsers.UserName;
                _GetMenuViewModel.Version = "1.1.702.2021";

                var response = await _AbsBusiness.GetDataAsync (_User);

                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (IEnumerable<UserMenu>) response.data;
                    var result = (from ViewTable in _Table select new GetMenuUserWise {
                        MenuId = ViewTable.Id,
                            MenuName = ViewTable.Name,
                            MenuAlias = ViewTable.Alias,
                            MenuView = ViewTable.View,
                            MenuUrl=ViewTable.userMenuModule.Name+ '_' + ViewTable.userMenuSubCategory.Name+ '/' + ViewTable.Alias+'?'+ViewTable.Id,

                            MenuCategoryId = ViewTable.userMenuSubCategory.userMenuCategory.Id,
                            MenuCategoryName = ViewTable.userMenuSubCategory.userMenuCategory.Name,

                            MenuSubCategoryId = ViewTable.userMenuSubCategory.Id,
                            MenuSubCategoryName = ViewTable.userMenuSubCategory.Name,
                            MenuSubCategoryIcon = ViewTable.userMenuSubCategory.Icon,

                            ModuleId = ViewTable.ModuleId,
                            ModuleName = ViewTable.userMenuModule.Name,
                            ModuleIcon = ViewTable.userMenuModule.Icon

                        //Id = ViewTable.Id,
                        //     Name = ViewTable.Name,
                        //     ShortName=ViewTable.Alias,
                        //     Type = ViewTable.Type,
                        //     Active = ViewTable.Active,
                    }).ToList ();
                    _GetMenuViewModel.GetMenuUserWises = result;
                    //response.data = result;
                }

                apiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                apiResponse.data = _GetMenuViewModel;

                return apiResponse;
            }
            return null;
        }
        public Task<ApiResponse> ProcessGetById (Guid _Id,Guid _MenuId, ClaimsPrincipal _User) {
            // if (_AbsBusiness != null) {
            //     var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
            //     if (Convert.ToInt32 (response.statusCode) == 200) {
            //         var _Table = (UserMenu) response.data;
            //         var _ViewModel = new UserMenuViewByIdModel {
            //             Id = _Table.Id,
            //             Name = _Table.Name,
            //             Alias = _Table.Alias,
            //             Type = _Table.Type,
            //             Active = _Table.Active,
            //             View = _Table.View,
            //             CompanyName = _Table.Companies.Name
            //         };
            //         response.data = _ViewModel;
            //     }
            //     return response;
            // }
            // return null;
            throw new NotImplementedException ();
        }
        public Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {
            // if (_AbsBusiness != null) {
            //     var _request = (UserMenuAddModel) request;
            //     var _Table = new UserMenu {
            //         Name = _request.Name,
            //         Alias = _request.Alias,
            //         Type = _request.Type,
            //         Active = _request.Active,
            //         View = _request.View,
            //         CompanyId = _request.CompanyId

            //     };
            //     return await _AbsBusiness.AddAsync (_Table, _User);
            // }
            // return null;
            throw new NotImplementedException ();
        }
        public Task<ApiResponse> ProcessPut (object request,ClaimsPrincipal _User) {
            throw new NotImplementedException ();
            // var _request = (UserMenuUpdateModel) request;
            // if (_AbsBusiness != null) {
            //     var _Table = new UserMenu {
            //     Id = _request.Id,
            //     Alias = _request.Alias,
            //     Type = _request.Type,
            //     Active = _request.Active,
            //     View = _request.View,
            //     CompanyId = _request.CompanyId
            //     };
            //     return await _AbsBusiness.UpdateAsync (_Table, _User);
            // }
            // return null;

        }
        public Task<ApiResponse> ProcessDelete (object model, ClaimsPrincipal _User) {
            // if (_AbsBusiness != null)
            //     return await _AbsBusiness.DeleteAsync (_Id, _User);
            // return null;
            throw new NotImplementedException ();
        }

    }
}