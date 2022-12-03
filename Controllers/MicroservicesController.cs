using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Auth.Helpers;
using TWP_API_Auth.Processor;
using TWP_API_Auth.Services;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Controllers
{
    ///<summary>
    ///Menu
    ///</summary>

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class MicroservicesController : ControllerBase
    {
        private readonly IMicroservices _IMicroservices = null;
        public MicroservicesController(IMicroservices IMicroservices)
        {
            _IMicroservices = IMicroservices;
        }

        ///<summary>
        ///Get User Key Validation through JWT Token
        ///</summary>
        [HttpGet]
        [Route("UserKey")]
        public async Task<IActionResult> UserKey()
        {
            try
            {
                ApiResponse result = await _IMicroservices.UserKeyAsync(User);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End

        ///<summary>
        ///Get User Key Validation through Key
        ///</summary>
        [HttpGet]
        [Route("UserKeyVerification")]
        [AllowAnonymous]
        public async Task<IActionResult> UserKeyVerification([FromHeader] string Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.UserKeyVerificationAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End

        ///<summary>
        ///Get User Info Validation through Key
        ///</summary>
        [HttpGet]
        [Route("UserLoginInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> UserLoginInfo([FromHeader] string Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.UserLoginInfoAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End


        ///<summary>
        ///Get Menu Permission by Menu Id and Key
        ///</summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("MenuPermission")]
        public async Task<IActionResult> GetMenuPermission([FromHeader] Guid MenuId, [FromHeader] string Key)
        {
            try
            {
                var result = await _IMicroservices.UserMenuPermissionAsync(MenuId, Key);
                // if (result.statusCode == StatusCodes.Status200OK.ToString())
                // {
                return Ok(result);
                // }
                // return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End


        ///<summary>
        ///Get User Menus through Key
        ///</summary>
        [HttpGet]
        [Route("MenuInfo")]
        [AllowAnonymous]
        public async Task<IActionResult> MenuInfo([FromHeader] string Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.MenuInfoAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End


        ///<summary>
        ///Get Branch Information
        ///</summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("BranchInfo")]
        public async Task<IActionResult> BranchInfo([FromHeader] string Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.BranchInfoAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End

        //Get End
        ///<summary>
        ///Get FinancialYear 
        ///</summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("FinancialYear")]
        public async Task<IActionResult> FinancialYear([FromHeader] string Key)
        {
            try
            {
                ApiResponse result = await _IMicroservices.FinancialYearAsync(Key);
                if (result.statusCode == StatusCodes.Status200OK.ToString())
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception e)
            {
                string innerexp = "";
                if (e.InnerException != null)
                {
                    innerexp = " Inner Error : " + e.InnerException.ToString();
                }
                return BadRequest(e.Message.ToString() + innerexp);
            }
        }
        //Get End

    }

}