using System;
using System.Threading.Tasks;
using TWP_API_Auth.Processor;
using TWP_API_Auth.Services;
using TWP_API_Auth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace TWP_API_Auth.Controllers {
    ///<summary>
    ///Users
    ///</summary>

    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]

    public class UserController : ControllerBase {
        private readonly IProcessor<UserBaseModel> _IProcessor = null;
        
        public UserController (IProcessor<UserBaseModel> IProcessor) {
            _IProcessor = IProcessor;
        }

        ///<summary>
        ///Get All Record
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetUser (Guid _MenuId) {
            try {
                var result = await _IProcessor.ProcessGet ( _MenuId,User);
                return Ok (result);

            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }
        //Get End

        ///<summary>
        ///Get Record by id
        ///</summary>
        [HttpGet]
        [Route ("GetUserById")]
        public async Task<IActionResult> GetUserById (Guid _MenuId,[FromHeader] Guid _Id) {
            try {
                var result = await _IProcessor.ProcessGetById (_Id, _MenuId,User);
                return Ok (result);

            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }
        //Get by Id End

        ///<summary>
        ///Save Record
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> AddUser ([FromBody] UserAddModel model) {
            try {
                return Ok (await _IProcessor.ProcessPost (model, User));
            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }
        //Create End

        //Update Start
        ///<summary>
        ///Update Record
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUser ([FromBody] UserUpdateModel model) {
            try {
                return Ok (await _IProcessor.ProcessPut (model, User));
            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }
        //Update End

        //Delete Start
        ///<summary>
        ///Delete Record
        ///</summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteUser ([FromBody] UserDeleteModel model) {
            try {
                return Ok (await _IProcessor.ProcessDelete (model, User));
            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }
        //Delete End
    }
}