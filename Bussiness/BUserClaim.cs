using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Bussiness
{
    public class BUserClaim : AbsBusiness
    {

        UserManager<ApplicationUser> _userManger;

        public BUserClaim(UserManager<ApplicationUser> userManager)
        {
            _userManger = userManager;
        }

        public override Task<ApiResponse> GetDataAsync( ClaimsPrincipal _User)
        {
            return null;
        }
        public override async Task<ApiResponse> GetDataByIdAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var user = await _userManger.FindByIdAsync(_Id.ToString());
                if (user == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "User Not Exist";
                    return ApiResponse;
                }
                
                var claims = await _userManger.GetClaimsAsync(user);
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = claims;
                return ApiResponse;
            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public override  Task<ApiResponse> AddAsync(object model, ClaimsPrincipal _User)
        {
            // var ApiResponse = new ApiResponse();
            // try
            // {
            //     GetUsr.GetUserDetail(_User).TryGetValue("UserId", out _UserId);

            //     var _model = (UserClaim)model;
            //     var user = await _userManger.FindByIdAsync(_model.UserId);

            //     await _userManger.AddClaimsAsync(user, _model.ClaimType.Where(c => c.IsSelected).Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimType)));
            //     //var result = await _userManger.AddClaimsAsync(user, GPCliams.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimType)));
            //     ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
            //     ApiResponse.message = "Record Saved";
            //     return ApiResponse;

            // }
            // catch (Exception e)
            // {

            //     string innerexp = "";
            //     if (e.InnerException != null)
            //     {
            //         innerexp = " Inner Error : " + e.InnerException.ToString();
            //     }
            //     ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
            //     ApiResponse.message = e.Message.ToString() + innerexp;
            //     return ApiResponse;
            // }
            return null;
        }
        public override async Task<ApiResponse> UpdateAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _model = (UserClaimsViewModel)model;
                var user = await _userManger.FindByIdAsync(_model.UserId);
                if (user == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "User Not Exist";
                    return ApiResponse;
                }
                var claims = await _userManger.GetClaimsAsync(user);
                var result = await _userManger.RemoveClaimsAsync(user, claims);
                 result = await _userManger.AddClaimsAsync(user, _model.Cliams.Select(c => new System.Security.Claims.Claim(c.ClaimType, c.ClaimType)));

                if (!result.Succeeded)
                {
                    return new ApiResponse
                    {
                        statusCode = StatusCodes.Status500InternalServerError.ToString(),
                        message = "Cannot add selected claims  to user",
                        error = result.Errors.Select(e => e.Description)
                    };
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Updated";
                return ApiResponse;
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
        public override async Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
              var user = await _userManger.FindByIdAsync(_Id.ToString());
                if (user == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "User Not Exist";
                    return ApiResponse;
                }
                var claims = await _userManger.GetClaimsAsync(user);
                var result = await _userManger.RemoveClaimsAsync(user, claims);
               
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Deleted";
                return ApiResponse;

            }
            catch (Exception e)
            {

                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = e.Message.ToString() + innerexp;
                return ApiResponse;
            }
        }
    }
}