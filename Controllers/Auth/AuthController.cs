using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TWP_API_Auth.Services;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Controllers
{
    ///<summary>
    ///Authorization
    ///</summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService _userService;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService userService, IMailService mailService, IConfiguration configuration)
        {
            _userService = userService;
            _mailService = mailService;
            _configuration = configuration;
        }

        // /api/auth/login
        ///<summary>
        ///User Login
        ///</summary>
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                // await _mailService.SendEmailAsync (model.Email, "New login", "<h1>Hey!, new login to vsign application from your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                return Ok(result);
            }

            return BadRequest("Some properties are not valid");
        }

        ///<summary>
        ///Confirm Email
        ///</summary>
        [HttpGet("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromHeader] string userId, [FromHeader] string token)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(userId))
                    return BadRequest("Please Provide User ID.");

                if (string.IsNullOrWhiteSpace(token))
                    return BadRequest("Please Provide Token.");

                var result = await _userService.ConfirmEmailAsync(userId, token);

                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///OTP Verification
        ///</summary>
        [HttpPost("OTPVerification")]
        [AllowAnonymous]
        public async Task<IActionResult> OTPVerification([FromBody] LoginTwoFactorModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginTwoFactorAsync(model);

                // await _mailService.SendEmailAsync (model.Email, "New login", "<h1>Hey!, new login to vsign application from your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                return Ok(result);
            }

            return BadRequest("Some properties are not valid");
        }
        ///<summary>
        ///Forget Password
        ///</summary>
        [HttpPut("ForgetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgetPassword([FromBody] ForgetPasswordViewModel ForgetPasswordViewModels)
        {

            var result = await _userService.ForgetPasswordAsync(ForgetPasswordViewModels);

            return Ok(result); // 200

        }

        ///<summary>
        ///Reset Password
        ///</summary>
        [HttpPut("ResetPassword")]
        //[AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordViewModel ResetPasswordViewModels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userService.ResetPasswordAsync(ResetPasswordViewModels, User);
                    return Ok(result);
                }
                return BadRequest("Some properties are not valid");

            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        ///<summary>
        ///Login Next
        ///</summary>
        [HttpPost("LoginNext")]
        [Authorize]
        public async Task<IActionResult> LoginNext([FromBody] LoginNextModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginNext(model, User);

                // await _mailService.SendEmailAsync (model.Email, "New login", "<h1>Hey!, new login to vsign application from your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                return Ok(result);
            }

            return BadRequest("Some properties are not valid");
        }

        ///<summary>
        ///Logout
        ///</summary>
        [HttpGet("LogoutUser")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutUser()
        {
            try
            {
                string _Token = HttpContext.Request.Headers["Authorization"].ToString();

                var result = await _userService.LogoutUserAsync(_Token);
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        ///<summary>
        ///User Login Details
        ///</summary>
        [HttpGet("UserLoginInfo")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> UserLoginInfo()
        {
            try
            {
                string _Token = HttpContext.Request.Headers["Authorization"].ToString();

                var result = await _userService.UserLoginInfoAsync(_Token);
                return Ok(result);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}