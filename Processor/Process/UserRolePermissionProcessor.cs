using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor {
    public class UserRolePermissionProcessor : IProcessor<UserRolePermissionBaseModel> {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRolePermissionProcessor (App_Data.DataContext context, UserManager<ApplicationUser> userManager) {
            _context = context;
            _userManager = userManager;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.RolePermission, _context, null, _userManager);
        }
        public Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {

            return null;
        }
        public async Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (RoleViewByIdModel) response.data;

                    response.data = _Table;
                }
                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserRolePermissionAddModel) request;

                var UserRolePermissions = new List<UserRolePermission> ();

                foreach (var item in _request.UserRolePermissions) {
                    var _Table = new UserRolePermission {
                        View_Permission = item.View_Permission,
                        Insert_Permission = item.Insert_Permission,
                        Update_Permission = item.Update_Permission,
                        Delete_Permission = item.Delete_Permission,
                        Print_Permission = item.Print_Permission,
                        Check_Permission = item.Check_Permission,
                        Approve_Permission = item.Approved_Permission,
                        Roles_Id = item.Roles_Id,
                        Menu_Id = item.Menu_Id
                    };

                    UserRolePermissions.Add (_Table);
                }

                return await _AbsBusiness.AddAsync (UserRolePermissions, _User);
            }

            return null;
        }
        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserRolePermissionUpdateModel) request;

                var UserRolePermissions = new List<UserRolePermission> ();

                foreach (var item in _request.UserRolePermissions) {
                    var _Table = new UserRolePermission {
                        View_Permission = item.View_Permission,
                        Insert_Permission = item.Insert_Permission,
                        Update_Permission = item.Update_Permission,
                        Delete_Permission = item.Delete_Permission,
                        Roles_Id = item.Roles_Id,
                        Menu_Id = item.Menu_Id
                    };

                    UserRolePermissions.Add (_Table);
                }

                return await _AbsBusiness.UpdateAsync (UserRolePermissions, _User);
            }
            return null;

        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserRolePermissionDeleteModel) request;

                Guid _Id = new Guid (_request.Roles_Id);
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}