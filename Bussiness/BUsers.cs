using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.Services;

namespace TWP_API_Auth.Bussiness
{
    public class BUsers : AbsBusiness
    {
        DataContext _context;
        String _UserId = "";

        private ErrorLog _ErrorLog = new ErrorLog();
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly IMailService _mailService;
        // private readonly IConfiguration _configuration;

        public BUsers(DataContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public override async Task<ApiResponse> GetDataAsync(ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                var _Table = await _context.Users.Include(x => x.Branches).Include(c => c.Branches.Companies).Where(a => a.Action != Enums.Operations.D.ToString()).ToListAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString(); ;
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }

                if (_Table.Count == 0)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record Not Found";
                    return ApiResponse;
                }

                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
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
        public override async Task<ApiResponse> GetDataByIdAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {

                var _Table = await _context.Users.Include(x => x.Branches).Include(c => c.Branches.Companies).Where(a => a.Id == _Id.ToString()).FirstOrDefaultAsync();

                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = "Record not found";
                    return ApiResponse;
                }
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.data = _Table;
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
        public override async Task<ApiResponse> AddAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();

            try
            {
                _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
                var _model = (ApplicationUser)model;

                bool emailExists = _context.Users.Any(rec => rec.Email.Trim().ToLower().Equals(_model.Email.Trim().ToLower()));
                if (emailExists)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "Email Already Exist";
                    return ApiResponse;
                }

                //var Password = SecurityHelper.PasswordGenerate();
                _model.UserName = _model.FirstName.Trim() + _model.LastName.Trim();
                _model.TwoFactorEnabled = false;
                _model.EmailConfirmed = true;
                _model.Type = Enums.Operations.S.ToString();
                _model.UserIdInsert = _UserId;
                _model.InsertDate = DateTime.Now;
                _model.Action = Enums.Operations.A.ToString();
                var result = await _userManager.CreateAsync(_model, _model.Password);
                if (result.Succeeded)
                {

                    ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                    ApiResponse.message = "User created successfully!";
                    ApiResponse.data = _model.Id;



                }
                else
                {
                    ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                    ApiResponse.error = result.Errors.Select(e => e.Description);
                }

                return ApiResponse;
            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;

            }
        }
        public override async Task<ApiResponse> UpdateAsync(object model, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            try
            {
                //GetUsr.GetUserDetail(_User).TryGetValue("UserId", out _UserId);
                var _model = (ApplicationUser)model;

                bool emailExists = _context.Users.Any(rec => rec.Email.Trim().ToLower().Equals(_model.Email.Trim().ToLower()) && rec.Id != _model.Id);
                if (emailExists)
                {
                    ApiResponse.statusCode = StatusCodes.Status409Conflict.ToString();
                    ApiResponse.message = "Email Already Exist";
                    return ApiResponse;
                }

                // Find the user by ID
                var result = await _userManager.FindByIdAsync(_model.Id);
                if (result == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = $"User with Id = {_model.Id} cannot be found";
                    return ApiResponse;
                }

                result.FirstName = _model.FirstName;
                result.LastName = _model.LastName;
                result.NormalizedUserName = _model.NormalizedUserName;
                result.NormalizedEmail = _model.NormalizedEmail;
                result.UserName = _model.UserName;
                result.AllBranchCheck = _model.AllBranchCheck;
                // result.PermissionDateFrom = _model.PermissionDateFrom;
                // result.PermissionDateTo = _model.PermissionDateTo;
                result.BranchId = _model.BranchId;
                result.UserIdInsert = _UserId;
                result.Email = _model.Email;
                result.EmployeeId = _model.EmployeeId;
                result.PhoneNumber = _model.PhoneNumber;
                result.Active = _model.Active;
                result.BackLog = _model.BackLog;
                result.Ck_RequiredAttandance = _model.Ck_RequiredAttandance;
                result.Ck_OnlineAttandance = _model.Ck_OnlineAttandance;
                _model.UserIdUpdate = _UserId;
                _model.UpdateDate = DateTime.Now;
                _model.Action = Enums.Operations.E.ToString();

                // Update the User using UpdateAsync
                var Result = await _userManager.UpdateAsync(result);

                if (Result.Succeeded)
                {

                    return new ApiResponse
                    {
                        statusCode = StatusCodes.Status200OK.ToString(),
                        message = "User updated successfully!"
                    };
                }
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status405MethodNotAllowed.ToString(),
                    error = Result.Errors.Select(e => e.Description)
                };

            }
            catch (Exception e)
            {

                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;

            }
        }
        public override async Task<ApiResponse> DeleteAsync(Guid _Id, ClaimsPrincipal _User)
        {
            var ApiResponse = new ApiResponse();
            _UserId = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.UserId.ToString())?.Value.ToString();
            try
            {
                // Find the user by Role ID
                //var user = await _userManager.FindByIdAsync(_Id.ToString());
                var _Table = _context.Users.Where(a => a.Id == _Id.ToString()).FirstOrDefault();
                if (_Table == null)
                {
                    ApiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    ApiResponse.message = $"User with Id = {_Id.ToString()} cannot be found";
                    return ApiResponse;
                }

                // Delete the user using DeleteAsync

                // var result = await _userManager.DeleteAsync(user);

                // if (result.Succeeded)
                //     return new ApiResponse
                //     {
                //         statusCode = StatusCodes.Status200OK.ToString(),
                //         message = "User deleted successfully!"
                //     };

                _Table.Action = Enums.Operations.D.ToString();
                _Table.UserIdDelete = _UserId;
                _Table.DeleteDate = DateTime.Now;

                // List<AuthClaim> authClaims = _context.AuthClaims.Where (a => a.Menu_Id == _Id).ToList ();
                // if (authClaims != null && authClaims.Count > 0) {
                //     foreach (var item in authClaims) {
                //         _context.AuthClaims.Remove (item);
                //     }
                // }

                await _context.SaveChangesAsync();
                ApiResponse.statusCode = StatusCodes.Status200OK.ToString();
                ApiResponse.message = "Record Delete : " + _Table.FirstName;
                return ApiResponse;

                // {
                //     statusCode = StatusCodes.Status405MethodNotAllowed.ToString(),
                //     error = result.Errors.Select(e => e.Description)
                // };

            }
            catch (Exception e)
            {
                string innerexp = e.InnerException == null ? e.Message : e.Message + " Inner Error : " + e.InnerException.ToString();
                string _ErrorId = await _ErrorLog.LogError("", innerexp, Enums.ErrorType.Error.ToString(), e.StackTrace, _User);
                ApiResponse.statusCode = StatusCodes.Status405MethodNotAllowed.ToString();
                ApiResponse.message = _ErrorId;
                return ApiResponse;

            }
        }
    }
}