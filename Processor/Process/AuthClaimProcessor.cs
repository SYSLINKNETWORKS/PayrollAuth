using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Processor.Process {
    public class AuthClaimProcessor : IProcessor<ClaimBaseModel> {
        private DataContext _context;
        private AbsBusiness _AbsBusiness;
        public AuthClaimProcessor (App_Data.DataContext context) {
            _context = context;
            _AbsBusiness = Builder.MakeBusinessClass (Enums.ClassName.AuthClaim, _context);
        }
        public async Task<ApiResponse> ProcessGet (Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataAsync (_User);

                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (IEnumerable<AuthClaim>) response.data;
                    var result = (from ViewTable in _Table select new ClaimViewModel {
                        Id = ViewTable.Id,
                            ClaimType = ViewTable.ClaimType,
                            Menu_Name = ViewTable.UserMenu.Alias
                    }).ToList ();
                    response.data = result;
                }
                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessGetById (Guid _Id, Guid _MenuId, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var response = await _AbsBusiness.GetDataByIdAsync (_Id, _User);
                if (Convert.ToInt32 (response.statusCode) == 200) {
                    var _Table = (AuthClaim) response.data;

                    var _ViewModel = new ClaimViewByIdModel {
                        Id = _Table.Id,
                        ClaimType = _Table.ClaimType,
                        Menu_Id = _Table.UserMenu.Id,
                        Menu_Name = _Table.UserMenu.Name,

                    };

                    response.data = _ViewModel;
                }
                return response;
            }
            return null;
        }
        public async Task<ApiResponse> ProcessPost (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (ClaimAddModel) request;
                var Id = new Guid ();
                var _Table = new AuthClaim {
                    Id = Id,
                    ClaimType = _request.ClaimType,
                    ClaimValue = _request.ClaimType,
                    Menu_Id = _request.Menu_Id
                };

                return await _AbsBusiness.AddAsync (_Table, _User);
            }

            return null;
        }
        public async Task<ApiResponse> ProcessPut (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (ClaimUpdateModel) request;

                var _Table = new AuthClaim {
                    Id = _request.Id,
                    ClaimType = _request.ClaimType,
                    ClaimValue = _request.ClaimType,
                    Menu_Id = _request.Menu_Id
                };

                return await _AbsBusiness.UpdateAsync (_Table, _User);
            }

            return null;
        }
        public async Task<ApiResponse> ProcessDelete (object request, ClaimsPrincipal _User) {
            if (_AbsBusiness != null) {
                var _request = (ClaimDeleteModel) request;

                Guid _Id = _request.Id;
                Guid _MenuId = _request.Menu_Id;

                return await _AbsBusiness.DeleteAsync (_Id, _User);
            }
            return null;
        }

    }
}