using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor.Process {
    public class RoleProcessor : IProcessor<RoleBaseModel> {
        private readonly RoleManager<IdentityRole> _roleManager;

        private AbsBusiness _AbsBusiness;
        private SecurityHelper _SecurityHelper = new SecurityHelper ();
        public RoleProcessor (RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;

            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.Role, null, _roleManager, null);
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
                    var _Table = (IEnumerable<IdentityRole>) response.data;
                    var result = (from ViewTable in _Table select new RoleViewModel {
                        Id = ViewTable.Id,
                            Name = ViewTable.Name,
                            NewPermission = _UserMenuPermission.Insert_Permission,
                            UpdatePermission = _UserMenuPermission.Update_Permission,
                            DeletePermission = _UserMenuPermission.Delete_Permission,

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
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                apiResponse = _SecurityHelper.UserMenuPermission (_MenuId, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Update_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (IdentityRole) response.data;

                    var _ViewModel = new RoleViewByIdModel {
                        Id = _Table.Id,
                        Name = _Table.Name,

                    };

                    response.data = _ViewModel;
                }
                return response;
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }
        public async Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                var _request = (RoleAddModel) request;

                apiResponse = _SecurityHelper.UserMenuPermission (_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Insert_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                var _Table = new IdentityRole {
                    Name = _request.Name,
                };

                return await _AbsBusiness.AddAsync (_Table, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                var _request = (RoleUpdateModel) request;

                apiResponse = _SecurityHelper.UserMenuPermission (_request.Menu_Id, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Update_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }

                var _Table = new IdentityRole {
                    Id = _request.Id,
                    Name = _request.Name,

                };

                return await _AbsBusiness.UpdateAsync (_Table, _User);

            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            ApiResponse apiResponse = new ApiResponse ();
            if (_AbsBusiness != null) {
                var _request = (RoleDeleteModel) request;

                Guid _Id = new Guid (_request.Id);
                Guid _MenuId = _request.Menu_Id;
                apiResponse = _SecurityHelper.UserMenuPermission (_MenuId, _User);
                if (apiResponse.statusCode.ToString () != StatusCodes.Status200OK.ToString ()) { return apiResponse; }
                var _UserMenuPermission = (GetUserPermissionViewModel) apiResponse.data;

                if (!_UserMenuPermission.Delete_Permission) {
                    apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString ();
                    return apiResponse;
                }
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            apiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
            apiResponse.message = "Invalid Class";
            return apiResponse;
        }

    }
}