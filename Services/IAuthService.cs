using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TWP_API_Auth.App_Data;
using TWP_API_Auth.Data;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Models;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Services
{
    public interface IAuthService
    {
        Task<ApiResponse> LoginUserAsync(LoginViewModel model);
        Task<ApiResponse> LoginTwoFactorAsync(LoginTwoFactorModel model);
        Task<ApiResponse> LoginNext(LoginNextModel model, ClaimsPrincipal _User);

        Task<ApiResponse> ConfirmEmailAsync(string userId, string token);
        Task<ApiResponse> ForgetPasswordAsync(ForgetPasswordViewModel _ForgetPasswordViewModels);
        Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel _ResetPasswordViewModel, ClaimsPrincipal _User);
        Task<ApiResponse> LogoutUserAsync(string token);

        Task<ApiResponse> UserLoginInfoAsync(string token);

    }

    public class AuthService : IAuthService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly DataContext _context;
        private readonly DbInitializer _DbInitializer;
        SecurityHelper _SecurityHelper = new SecurityHelper();
        private NotificationService _NotificationService = null;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IMailService mailService, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mailService = mailService;
            _context = context;
            _DbInitializer = new DbInitializer(_context);
            _NotificationService = new NotificationService();
        }

        public async Task<ApiResponse> LoginUserAsync(LoginViewModel model)
        {
            ApiResponse apiResponse = new ApiResponse();
            await _DbInitializer.Initialize();

            var _user = await _userManager.FindByEmailAsync(model.Email);

            if (_user == null)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "There is no user with that Email address";
                return apiResponse;
            }
            if (!_user.EmailConfirmed)
            {
                apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                apiResponse.message = "Please Confirm You Email.";
                return apiResponse;
            }
            var result = await _userManager.CheckPasswordAsync(_user, model.Password);

            if (!result)
            {
                await _userManager.AccessFailedAsync(_user);
                apiResponse.statusCode = StatusCodes.Status401Unauthorized.ToString();
                apiResponse.message = "Invalid email / password";
                return apiResponse;

            }

            var _IsLockOut = await _userManager.IsLockedOutAsync(_user);
            //var result_two = await _userManager.SetTwoFactorEnabledAsync (_user, true);
            if (_IsLockOut)
            {
                await _userManager.AccessFailedAsync(_user);
                apiResponse.statusCode = StatusCodes.Status203NonAuthoritative.ToString();
                apiResponse.message = "Account is locked. Try after " + _user.LockoutEnd.Value.ToLocalTime();
                return apiResponse;

            }
            if (!_user.Active)
            {
                apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                apiResponse.message = "Account is in-active.";
                return apiResponse;
            }


            if (_user.TwoFactorEnabled)
            {
                //_userManager.RegisterTokenProvider ("Email Code", new EmailTokenProvider<ApplicationUser>());

                var tokenTwoFactor = await _userManager.GenerateTwoFactorTokenAsync(_user, "Email");
                SendGridMailService sendGridMailService = new SendGridMailService(_configuration);

                await sendGridMailService.SendEmailConfirmationAsync(model.Email, "OTP", $"<h1>Code :{tokenTwoFactor} </h1>");
                await GetToken(model.Email, model.WanIp, model.Header);
                // Log.Error ("User Login End : " + DateTime.Now.ToString ());

                apiResponse.statusCode = StatusCodes.Status200OK.ToString();
                apiResponse.message = "Check your email for Code";
                return apiResponse;
            }

            //Check Attendance for Login
            if (_user.Ck_RequiredAttandance)
            {
            }
            if (!_user.TwoFactorEnabled)
            {
                var TokenString = await GetToken(model.Email, model.WanIp, model.Header);
                // if (_user.Ck_RequiredAttandance)
                // {
                //     if (!_user.EmployeeId.HasValue)
                //     {
                //         apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                //         apiResponse.message = "Employee not found";
                //         return apiResponse;
                //     }

                //     var _LoginAttendanceResponse = await _SecurityHelper.LoginAttendanceAsync(_user.EmployeeId.Value);
                //     if (_LoginAttendanceResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _LoginAttendanceResponse; }
                //     if (!Convert.ToBoolean(apiResponse.message))
                //     {
                //         apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                //         apiResponse.message = "Attendance not found";
                //         return apiResponse;
                //     }

                // }


                apiResponse = await GetUserDetails(model.Email, TokenString);
                //MarkAttendance
                if (model.latitude > 0 && model.longitude > 0)
                {
                    var _EmployeesResponse = await _SecurityHelper.GetEmployeesForLoginAsync("Bearer " + TokenString);
                    if (_EmployeesResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _EmployeesResponse; }
                    var _EmployeeViewModel = (List<EmployeesMicroServiceViewModel>)_EmployeesResponse.data;

                    var _EmployeeTable = _EmployeeViewModel.Where(x => x.Id == _user.EmployeeId).FirstOrDefault();
                    if (_EmployeeTable == null)
                    {
                        apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                        apiResponse.message = "Employee not found";
                        return apiResponse;

                    }
                    if (!_user.Ck_OnlineAttandance)
                    {
                        apiResponse.statusCode = StatusCodes.Status403Forbidden.ToString();
                        apiResponse.message = "Un-Authorize for online attendance";
                        return apiResponse;

                    }
                    apiResponse = await MarkAttendance(model.latitude, model.longitude, _EmployeeTable.MachineId, _EmployeeTable.Name + ' ' + _EmployeeTable.FatherName, _EmployeeTable.Id, "Bearer " + TokenString);

                    // var _EmployeeTable = await _context.Employees.Where(x => x.Id == _user.EmployeeId).FirstOrDefaultAsync();
                    // if (_EmployeeTable == null)
                    // {
                    //     apiResponse.statusCode = StatusCodes.Status404NotFound.ToString();
                    //     apiResponse.message = "Employee not found";
                    //     return apiResponse;

                    // }
                    // await MarkAttendance(_user.Id, model.latitude, model.longitude, _EmployeeTable.MachineId, _EmployeeTable.Name + ' ' + _EmployeeTable.FatherName, _EmployeeTable.Id);

                }

                return apiResponse;
            }

            apiResponse.statusCode = StatusCodes.Status400BadRequest.ToString();
            apiResponse.message = "Error found";
            return apiResponse;

        }
        public async Task<ApiResponse> LoginTwoFactorAsync(LoginTwoFactorModel model)
        {
            var _user = await _userManager.FindByEmailAsync(model.Email);
            bool verified = await _userManager.VerifyTwoFactorTokenAsync(_user, "Email", model.Code); // .TwoFactorSignInAsync ("Email", model.Code, false, false); // .TwoFactorSignInAsync ("Email", model.Code, false, false); // .TwoFactorAuthenticatorSignInAsync (model.Code, false, false);
            // if (!result .Succeeded) {
            if (!verified)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status403Forbidden.ToString(),
                    message = "Invalid code" // result.ToString () // "There is no user with that Email address",                    
                };

            }

            var TokenString = await GetToken(model.Email, model.WanIp, model.Header);

            return await GetUserDetails(model.Email, TokenString);
        }

        public async Task<ApiResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)

                return new ApiResponse
                {
                    statusCode = StatusCodes.Status200OK.ToString(),
                    message = "Email confirmed successfully!"
                };

            return new ApiResponse
            {
                statusCode = StatusCodes.Status405MethodNotAllowed.ToString(),
                error = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<ApiResponse> ForgetPasswordAsync(ForgetPasswordViewModel _ForgetPasswordViewModels)
        {
            var user = await _context.Users.Where(x => x.Id == _ForgetPasswordViewModels.Id).FirstOrDefaultAsync();
            if (user == null)
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "Record not found",
                };

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, code, _ForgetPasswordViewModels.Password);

            if (result.Succeeded)
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status200OK.ToString(),
                    message = "Password has been reset successfully!"
                };

            return new ApiResponse
            {
                statusCode = StatusCodes.Status405MethodNotAllowed.ToString(),
                error = result.Errors.Select(e => e.Description),
            };
        }
        public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordViewModel _ResetPasswordViewModel, ClaimsPrincipal _User)
        {
            var _UserEmail = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Email.ToString())?.Value.ToString();


            var user = await _userManager.FindByEmailAsync(_UserEmail);
            if (user == null)
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "No user associated with email",
                };


            var resultLogin = await _userManager.CheckPasswordAsync(user, _ResetPasswordViewModel.OldPassword);
            if (!resultLogin)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status403Forbidden.ToString(),
                    message = "Invalid Old Password ",
                };
            }

            string code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var result = await _userManager.ResetPasswordAsync(user, code, _ResetPasswordViewModel.Password);

            if (result.Succeeded)
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status200OK.ToString(),
                    message = "Password has been reset successfully!"
                };

            return new ApiResponse
            {
                statusCode = StatusCodes.Status405MethodNotAllowed.ToString(),
                error = result.Errors.Select(e => e.Description),
            };
        }
        public async Task<ApiResponse> LogoutUserAsync(string _TokenString)
        {

            var _Token = _TokenString.Substring(7, _TokenString.Length - 7);
            var _Handler = new JwtSecurityTokenHandler();
            var _TokenDecode = _Handler.ReadJwtToken(_Token);
            string _Key = _TokenDecode.Audiences.ToList()[0].ToString();

            var _UserLoginAudits = await _context.UserLoginAudits.Where(x => x.Key == _Key).FirstOrDefaultAsync();
            if (_UserLoginAudits == null)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "Invalid User"
                };

            }
            _UserLoginAudits.Status = false;
            _UserLoginAudits.Key = "";
            _context.SaveChanges();

            return new ApiResponse
            {
                statusCode = StatusCodes.Status200OK.ToString(),
                message = "User Logout successfully!"
            };
        }

        private async Task<string> GetToken(string email, string _WanIp, string _Header)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var UserTable = await _context.Users.Include(b => b.Branches).Include(c => c.Branches.Companies).Where(x => x.Id == user.Id).FirstOrDefaultAsync();
            string _EmployeeId = "";

            string _key = _SecurityHelper.security(user.Id + user.UserName + user.Email + DateTime.Now.ToString("ddMMMyyyyHHHmmss"));

            // var _Table = from _user in user
            // join _Company in Company
            // equals new { _user.FirstName, student.LastName }
            var _UserRoles = await _userManager.GetRolesAsync(user);

            string key = _configuration["AuthSettings:Key"];
            var issuer = _configuration["AuthSettings:Issuer"];
            var audience = _key; //_configuration["AuthSettings:Audience"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var _UserClaims1 = await _userManager.GetClaimsAsync(user);
            await _userManager.RemoveClaimsAsync(user, _UserClaims1);

            _EmployeeId = user.EmployeeId.HasValue ? user.EmployeeId.ToString() : "";

            var permClaims = new List<System.Security.Claims.Claim>();
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.CompanyId.ToString(), UserTable.Branches.CompanyId.ToString()));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.CompanyName.ToString(), UserTable.Branches.Companies.Name));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.UserId.ToString(), user.Id));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.UserName.ToString(), user.UserName));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.Email.ToString(), user.Email));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.EmployeeId.ToString(), _EmployeeId));
            permClaims.Add(new System.Security.Claims.Claim(Enums.Misc.Key.ToString(), _key));

            var _UserClaims = await _userManager.GetClaimsAsync(user);
            if (_UserClaims.Count > 0)
            {
                foreach (var _Claims in _UserClaims)
                {
                    permClaims.Add(new System.Security.Claims.Claim(_Claims.Type, _Claims.Value));
                }
            }

            var token = new JwtSecurityToken(issuer,
                audience,
                permClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            var _Branch = await _context.Branches.Where(x => x.Id == user.BranchId).FirstOrDefaultAsync();

            UserLoginAudit _UserLoginAudit = new UserLoginAudit();
            _UserLoginAudit.UserId = user.Id;
            _UserLoginAudit.Key = _key;
            _UserLoginAudit.WanIp = _WanIp;
            _UserLoginAudit.Header = _Header;
            _UserLoginAudit.CompanyId = _Branch.CompanyId;
            _UserLoginAudit.BranchId = user.BranchId;
            _UserLoginAudit.Status = true;
            _context.UserLoginAudits.Add(_UserLoginAudit);
            _context.SaveChanges();

            return tokenAsString;
        }
        private async Task<ApiResponse> GetUserDetails(string email, string tokenAsString)
        {
            var user = await _userManager.FindByEmailAsync(email);

            UserDetailsViewModel userDetails = new UserDetailsViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BranchName = user.BranchId.ToString()
            };

            return new ApiResponse
            {
                statusCode = StatusCodes.Status200OK.ToString(),
                message = tokenAsString,
                data = userDetails
            };

        }
        public async Task<ApiResponse> LoginNext(LoginNextModel model, ClaimsPrincipal _User)
        {

            var _UserKey = _User.Claims.FirstOrDefault(c => c.Type == Enums.Misc.Key.ToString())?.Value.ToString();
            var _EmpolyeeIdKey = _User.Claims.FirstOrDefault(x => x.Type == Enums.Misc.EmployeeId.ToString())?.Value.ToString();
            Guid _EmployeeId = Guid.Empty;
            bool _CkSalesman = false, _CkDirector = false;

            var _UserLoginAudits = await _context.UserLoginAudits.Where(x => x.Key == _UserKey).FirstOrDefaultAsync();
            if (_UserLoginAudits == null)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status403Forbidden.ToString(),
                    message = "Invalid Token"
                };
            }

            var _Branch = await _context.Branches.Where(b => b.Id == model.BranchId).Include(c => c.Companies).FirstOrDefaultAsync();
            if (_Branch == null)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "Branch not found"
                };
            }
            var _Year = await _context.FinancialYears.Where(b => b.Id == model.YearId).FirstOrDefaultAsync();
            if (_Year == null)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status404NotFound.ToString(),
                    message = "Financial Year not found"
                };
            }
            //Get EmployeeById 
            if (!string.IsNullOrEmpty(_EmpolyeeIdKey))
            {
                _EmployeeId = new Guid(_EmpolyeeIdKey);
                var _EmployeeApiResponse = await _SecurityHelper.GetEmployeeByIdAsync(_UserKey, _EmployeeId);
                if (_EmployeeApiResponse.statusCode != StatusCodes.Status200OK.ToString()) { return _EmployeeApiResponse; }
                var _EmployeeViewModel = (EmployeeByIdMicroServiceViewModel)_EmployeeApiResponse.data;

                _CkSalesman = _EmployeeViewModel.Salesman;
                _CkDirector = _EmployeeViewModel.Director;
            }

            _UserLoginAudits.CompanyId = _Branch.CompanyId;
            _UserLoginAudits.BranchId = _Branch.Id;
            _UserLoginAudits.YearId = _Year.Id;
            _UserLoginAudits.Status = true;
            _UserLoginAudits.EmployeeId = _EmployeeId;
            _UserLoginAudits.CkSalesman = _CkSalesman;
            _UserLoginAudits.CkDirector = _CkDirector;
            _context.SaveChanges();

            return new ApiResponse
            {
                statusCode = StatusCodes.Status200OK.ToString()
            };
        }

        public async Task<ApiResponse> UserLoginInfoAsync(string _TokenString)
        {

            var _Token = _TokenString.Substring(7, _TokenString.Length - 7);
            var _Handler = new JwtSecurityTokenHandler();
            var _TokenDecode = _Handler.ReadJwtToken(_Token);
            string _Key = _TokenDecode.Audiences.ToList()[0].ToString();

            var _UserLoginAudits = await _context.UserLoginAudits.Where(x => x.Key == _Key).Include(u => u.ApplicationUsers).Include(b => b.Branches).Include(c => c.Companies).Include(y => y.FinancialYears).FirstOrDefaultAsync();
            if (_UserLoginAudits == null)
            {
                return new ApiResponse
                {
                    statusCode = StatusCodes.Status401Unauthorized.ToString(),
                    message = "Invalid User"
                };

            }

            var _Table = new UserLoginInfoBaseModel
            {
                UserId = _UserLoginAudits.UserId,
                UserName = _UserLoginAudits.ApplicationUsers.FirstName + " " + _UserLoginAudits.ApplicationUsers.LastName,
                CompanyId = _UserLoginAudits.CompanyId,
                CompanyName = _UserLoginAudits.Companies.Name,
                BranchId = _UserLoginAudits.BranchId,
                BranchName = _UserLoginAudits.Branches.Name,
                YearId = _UserLoginAudits.YearId.Value,
                YearName = _UserLoginAudits.FinancialYears.StartYear + "/" + _UserLoginAudits.FinancialYears.EndYear,
                YearStartDate = _UserLoginAudits.FinancialYears.StartDate.ToString(),
                YearEndDate = _UserLoginAudits.FinancialYears.EndDate.ToString(),

            };

            return new ApiResponse
            {
                statusCode = StatusCodes.Status200OK.ToString(),
                data = _Table
            };
        }
        private async Task<ApiResponse> MarkAttendance(Double _latitude, double _longitude, int _MachineId, string _EmployeeName, Guid _CompanyId, string _TokenString)
        {
            ApiResponse apiResponse = new ApiResponse();

            CheckInOutMachineListModel _model = new CheckInOutMachineListModel();
            _model.CheckInOutMachineModels = new List<CheckInOutMachineModel>();
            _model.CheckInOutMachineModels.Add(new CheckInOutMachineModel
            {
                UserId = _MachineId,
                CheckTime = DateTime.Now,
                Latitude = _latitude,
                Longitude = _longitude,

            });



            var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", false)
                        .Build();


            HttpClient client = new HttpClient();

            var _BaseUri = configuration["ConnectionStrings:GWAPI"] + "/Payroll/v1/";
            client.BaseAddress = new Uri(_BaseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", _TokenString);
            var url = "PayrollDashboard/AttendanceMachinePost";
            var _CheckInOutMachineSerialize = JsonSerializer.Serialize(_model);
            var requestContent = new StringContent(_CheckInOutMachineSerialize, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(url, requestContent);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var _JsonData = response.Content.ReadAsStringAsync().Result;
                if (_JsonData != null)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();

                    ApiResponse _ApiResponse = JsonSerializer.Deserialize<ApiResponse>(content);// JsonConvert.DeserializeObject<ApiResponse>(_JsonData.ToString());
                    apiResponse = _ApiResponse;

                }
                return apiResponse;
            }
            return apiResponse;

            //            var httpClient = new HttpClient();
            // var json = await httpClient.GetStringAsync ("https://maps.google.com/maps/api/geocode/xml?key=AIzaSyC5VhMsnfo8GPFQA7t0w1f_IpE0u51UXGY&latlng=" + _latitude + "," + _longitude + "&sensor=false");
            // var responseXml = new XmlDocument ();
            // responseXml.LoadXml (json);

            // string _address = responseXml.SelectSingleNode ("//formatted_address").InnerText;

            // var _model = new CheckInOut ();
            // _model.MachineId = _MachineId;
            // _model.CheckTime = DateTime.Now;
            // _model.Date = Convert.ToDateTime (DateTime.Now.ToShortDateString ());
            // _model.Latitude = _latitude;
            // _model.Longitude = _longitude;
            // _model.Address = _address;
            // _model.Action = Enums.Operations.A.ToString ();
            // _model.CompanyId = _CompanyId;
            // _model.UserIdInsert = _Id;
            // _model.InsertDate = DateTime.Now;
            // _model.Type = Enums.Operations.M.ToString ();
            // _model.CheckType = Enums.Payroll.I.ToString ();
            // _model.Approved = true;
            // await _context.CheckInOuts.AddAsync (_model);
            // _context.SaveChanges ();

            // CheckAttendanceBaseModel _CheckAttendanceBaseModel = new CheckAttendanceBaseModel ();
            // _CheckAttendanceBaseModel.DateFrom = _model.Date;
            // _CheckAttendanceBaseModel.DateTo = _model.Date;
            // _CheckAttendanceBaseModel.MachineId = _MachineId;
            // AttendanceProcess _AttendanceProcess = new AttendanceProcess ();
            // await _AttendanceProcess.AttendanceProcessAsync (_CheckAttendanceBaseModel);
            // await _NotificationService.sendnotification ("1", "", Convert.ToDateTime (_model.CheckTime), _EmployeeName, "Attendance");

            //            return true;

        }

    }
}