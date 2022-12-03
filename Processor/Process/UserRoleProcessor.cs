using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor.Process {
    public class UserRoleProcessor : IProcessor<UserRolesViewModel> {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManager;

        private AbsBusiness _AbsBusiness;
        public UserRoleProcessor (UserManager<ApplicationUser> userManger, RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
            _userManger = userManger;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.UserRole, null, _roleManager, userManger);
        }
        public Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {
            return null;
        }

        public async Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    response.data = response.data; // _ViewModel;
                }
                return response;
            }
            return null;
        }
        public Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {
            return null;
        }
        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserRolesViewModel) request;

                return await _AbsBusiness.UpdateAsync (_request, _User);
            }

            return null;
        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserRolesViewModel) request;

                Guid _Id = new Guid (_request.UserId);
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}