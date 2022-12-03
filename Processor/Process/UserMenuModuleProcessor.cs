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
    public class UserMenuModuleProcessor : IProcessor<UserMenuModuleBaseModel> {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        private SecurityHelper _SecurityHelper = new SecurityHelper ();
        public UserMenuModuleProcessor (App_Data.DataContext context) {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.UserMenuModule, _context);
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
                    var _Table = (IEnumerable<UserMenuModule>) response.data;
                    var result = (from ViewTable in _Table select new UserMenuModuleViewModel {
                        Id = ViewTable.Id,
                            Name = ViewTable.Name,
                            Icon = ViewTable.Icon,
                            Type = ViewTable.Type,
                            Active = ViewTable.Active,
                            NewPermission = _UserMenuPermission.Insert_Permission,
                             UpdatePermission = _UserMenuPermission.Update_Permission, 
                             DeletePermission = _UserMenuPermission.Delete_Permission
                    }).ToList ();
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
                    var _Table = (UserMenuModule) response.data;
                    var _ViewModel = new UserMenuModuleViewByIdModel {
                        Id = _Table.Id,
                        Name = _Table.Name,
                        Icon = _Table.Icon,
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
                var _request = (UserMenuModuleAddModel) request;

                apiResponse = _SecurityHelper.UserMenuPermission (_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Insert_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                var _Table = new UserMenuModule {
                    Name = _request.Name,
                    Icon = _request.Icon,
                    Type = _request.Type,
                    Active = _request.Active,
                    CompanyId = _UserMenuPermission.CompanyId
                };
                return await _AbsBusiness.AddAsync (_Table, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
            // if (_AbsBusiness != null) {
            //     var _request = (UserMenuModuleAddModel) request;
            //     var _Table = new UserMenuModule {
            //         Name = _request.Name,
            //         Type = _request.Type,
            //         Active = _request.Active,
            //         CompanyId = "55FF776B-E12E-4A39-84A7-08D94797A455".
            //     };
            //     return await _AbsBusiness.AddAsync (_Table, _User);
            // }
            // return null;
        }
        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            var _request = (UserMenuModuleUpdateModel) request;
            if (_AbsBusiness != null) {
                var _Table = new UserMenuModule {
                Id = _request.Id,
                Name = _request.Name,
                Icon = _request.Icon,
                Type = _request.Type,
                Active = _request.Active,
                CompanyId = _request.CompanyId
                };
                return await _AbsBusiness.UpdateAsync (_Table, _User);
            }
            return null;

        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserMenuModuleDeleteModel) request;

                Guid _Id = _request.Id;
                Guid _MenuId = _request.Menu_Id;
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}