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
    public class UserClaimProcessor : IProcessor<UserClaimsViewModel> {
        private readonly UserManager<ApplicationUser> _userManger;

        private AbsBusiness _AbsBusiness;
        public UserClaimProcessor (UserManager<ApplicationUser> userManger) {
            _userManger = userManger;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.UserClaim, null, null, userManger);
        }
        public Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {
            return null;
        }

        public async Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (IList<Claim>) response.data;
                    List<UserClaim> claims = new List<UserClaim> ();
                    if (_Table.Count > 0) {
                        foreach (var item in _Table) {
                            var UserClaim = new UserClaim () {
                                ClaimType = item.Type
                            };

                            claims.Add (UserClaim);
                        }

                    }
                    var _ViewModel = new UserClaimsViewModel {
                        UserId = _Id.ToString (),
                        Cliams = claims
                    };
                    response.data = _ViewModel;
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
                var _request = (UserClaimsViewModel) request;

                return await _AbsBusiness.UpdateAsync (_request, _User);
            }

            return null;
        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (UserClaimsViewModel) request;

                Guid _Id = new Guid (_request.UserId);
                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}