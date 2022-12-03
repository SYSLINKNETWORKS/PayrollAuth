using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace TWP_API_Auth.Bussiness {
    public class BAuthClaim : AbsBusiness {
        DataContext _context;
        String _UserId = "";

        public BAuthClaim (DataContext context) {
            _context = context;
        }

        public override async Task<ApiResponse> GetDataAsync (ClaimsPrincipal _User) {
            var ApiResponse = new ApiResponse ();
            try {
                var _Table = await _context.AuthClaims.Include (m => m.UserMenu).ToListAsync ();

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
                var _Table = await _context.AuthClaims.Where (a => a.Id == _Id).Include (m => m.UserMenu).FirstOrDefaultAsync ();
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
                _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
                //GetUsr.GetUserDetail (_User).TryGetValue ("UserId", out _UserId);
                var _model = (AuthClaim) model;

                bool recordExists = _context.AuthClaims.Any (rec => rec.ClaimType.Trim ().ToLower ().Equals (_model.ClaimType.Trim ().ToLower ()));
                if (recordExists) {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    ApiResponse.message = "Claim Already Exist";
                    return ApiResponse;
                }
                _model.UserIdInsert = _UserId;
                _model.InsertDate = DateTime.Now;
                _model.Action = "A";
                await _context.AuthClaims.AddAsync (_model);

                _context.SaveChanges ();
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
                _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
                var _model = (AuthClaim) model;

                bool recordExists = _context.AuthClaims.Any (rec => rec.ClaimType.Trim ().ToLower ().Equals (_model.ClaimType.Trim ().ToLower ()) && rec.Id != _model.Id);
                if (recordExists) {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString ();
                    ApiResponse.message = "Claim Already Exist";
                    return ApiResponse;
                }

                var result = _context.AuthClaims.Where (a => a.Id == _model.Id).FirstOrDefault ();
                if (result == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record not found ";
                    return ApiResponse;
                }

                result.ClaimType = _model.ClaimType;
                result.ClaimValue = _model.ClaimValue;
                result.Menu_Id = _model.Menu_Id;
                _model.UserIdUpdate = _UserId;
                _model.UpdateDate = DateTime.Now;
                _model.Action = "E";

                await _context.SaveChangesAsync ();
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
            _UserId = _User.Claims.FirstOrDefault (c => c.Type == Enums.Misc.UserId.ToString ())?.Value.ToString ();
            try {
                var _Table = _context.AuthClaims.Where (a => a.Id == _Id).FirstOrDefault ();
                if (_Table == null) {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString ();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                _context.AuthClaims.Remove (_Table);

                await _context.SaveChangesAsync ();
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