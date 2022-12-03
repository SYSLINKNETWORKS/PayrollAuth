using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace TWP_API_Auth.Bussiness {
    public class BRole : AbsBusiness {
        private readonly RoleManager<IdentityRole> _roleManager;
        public BRole (RoleManager<IdentityRole> roleManager) {
            _roleManager = roleManager;
        }

        public override async Task<ApiResponse> GetDataAsync (ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                var _Table = await _roleManager.Roles.ToListAsync ();

                if (_Table == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }

                if (_Table.Count == 0) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString (); // "401";
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.data = _Table;
                return ApiResponse;

            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> GetDataByIdAsync (Guid _Id, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                //var _Table = await _roleManager.Roles.Where(o=>o.Id=_Id).FirstOrDefaultAsync();
                var _Table = await _roleManager.Roles.Where (a => a.Id == _Id.ToString ()).FirstOrDefaultAsync ();
                if (_Table == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.data = _Table;
                return ApiResponse;
            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> AddAsync (object model, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                var _model = (IdentityRole) model;
                bool recordExists = _roleManager.Roles.Any (rec => rec.Name.Trim ().ToLower ().Equals (_model.Name.Trim ().ToLower ()));
                if (recordExists) {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    ApiResponse.message = "Role Already Exist";
                    return ApiResponse;
                }

                await _roleManager.CreateAsync (_model);
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.message = "Record Saved";
                return ApiResponse;

            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> UpdateAsync (object model, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                var _model = (IdentityRole) model;

                bool recordExists = _roleManager.Roles.Any (rec => rec.Name.Trim ().ToLower ().Equals (_model.Name.Trim ().ToLower ()) && rec.Id != _model.Id);
                if (recordExists) {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    ApiResponse.message = "Claim Already Exist";
                    return ApiResponse;
                }

                var result = _roleManager.Roles.Where (a => a.Id == _model.Id).FirstOrDefault ();
                if (result == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }
                result.Name = _model.Name;
                await _roleManager.UpdateAsync (result);
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.message = "Record Updated";
                return ApiResponse;

            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> DeleteAsync (Guid _Id, ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                var _Table = await _roleManager.FindByIdAsync (_Id.ToString ());

                if (_Table == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                await _roleManager.DeleteAsync (_Table);
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString ();
                ApiResponse.message = "Record Deleted";
                return ApiResponse;

            } catch (Exception e) {

                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString ();
                ApiResponse.message = e.Message.ToString () + innerexp;
                return ApiResponse;
            }
        }
    }
}