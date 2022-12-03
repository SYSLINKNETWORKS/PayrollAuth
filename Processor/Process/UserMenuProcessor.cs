using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor {

    public class UserMenuProcessor : IProcessor<UserMenuBaseModel> {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;

        private SecurityHelper _SecurityHelper = new SecurityHelper ();
        public UserMenuProcessor (App_Data.DataContext context) {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.UserMenu, _context);
        }
        public async Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                apiResponse = _SecurityHelper.UserMenuPermission (_MenuId, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.View_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                var response = await _AbsBusiness.GetDataAsync (_User);

                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (IEnumerable<UserMenu>) response.data;
                    var result = (from ViewTable in _Table select new UserMenuViewModel {
                        Id = ViewTable.Id,
                            ModuleId = ViewTable.ModuleId,
                            ModuleName = ViewTable.userMenuModule.Name,
                            SubCategoryId = ViewTable.SubCategoryId,
                            SubCategoryName = ViewTable.userMenuSubCategory.Name,
                            Name = ViewTable.Name,
                            Alias = ViewTable.Alias,
                            Type = ViewTable.Type,
                            Active = ViewTable.Active,
                            NewPermission = _UserMenuPermission.Insert_Permission,
                            UpdatePermission = _UserMenuPermission.Update_Permission,
                            DeletePermission = _UserMenuPermission.Delete_Permission
                    }).OrderBy (mod => mod.ModuleName).ToList ();
                    response.data = result;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }
        public async Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (UserMenu) response.data;
                    var _ViewModel = new UserMenuViewByIdModel {
                        ModuleId = _Table.ModuleId,
                        Module_Name = _Table.userMenuModule.Name.Trim (),
                        SubCategoryId = _Table.SubCategoryId,
                        SubCategory_Name = _Table.userMenuSubCategory.Name.Trim (),
                        Id = _Table.Id,
                        Name = _Table.Name,
                        Alias = _Table.Alias,
                        Type = _Table.Type,
                        Active = _Table.Active
                    };
                    response.data = _ViewModel;
                }
                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {

            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {

                var _request = (UserMenuAddModel) request;
                apiResponse = _SecurityHelper.UserMenuPermission (_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Insert_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                var _Table = new UserMenu {
                    ModuleId = _request.ModuleId,
                    SubCategoryId = _request.SubCategoryId,
                    Name = _request.Name,
                    Alias = _request.Alias,
                    Type = _request.Type,
                    Active = _request.Active,
                    View = _request.View,
                    CompanyId = _UserMenuPermission.CompanyId
                };
                return await _AbsBusiness.AddAsync (_Table, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;

            //     if (_AbsBusiness != null) {
            //         var _request = (UserMenuAddModel) request;
            //         var _Table = new UserMenu {
            //             ModuleId =  _request.ModuleId,
            //             SubCategoryId =  _request.SubCategoryId,
            //             Name = _request.Name,
            //             Alias = _request.Alias,
            //             Type = _request.Type,
            //             Active = _request.Active,
            //             View = _request.View,
            //             CompanyId = _request.CompanyId
            //         };
            //         return await _AbsBusiness.AddAsync (_Table, _User);
            //     }
            //     return null;
            // }
        }
        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            var _request = (UserMenuUpdateModel) request;
            if (_AbsBusiness != null) {
                var _Table = new UserMenu {
                Id = _request.Id,
                ModuleId = _request.ModuleId,
                SubCategoryId = _request.SubCategoryId,
                Name = _request.Name,
                Alias = _request.Alias,
                Type = _request.Type,
                Active = _request.Active,
                View = _request.View,
                CompanyId = _request.CompanyId
                };
                return await _AbsBusiness.UpdateAsync (_Table, _User);
            }
            return null;

        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserMenuDeleteModel) request;

                Guid _Id = _request.Id;
                Guid _MenuId = _request.Menu_Id;
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}