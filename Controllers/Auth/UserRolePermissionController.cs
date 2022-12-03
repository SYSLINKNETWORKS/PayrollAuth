using System;
using System.Threading.Tasks;
using TWP_API_Auth.Processor;
using TWP_API_Auth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TWP_API_Auth.Controllers {
    ///<summary>
    ///User Role Permission Mapping
    ///</summary>

    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]
    public class UserRolePermissionController : ControllerBase {
        private readonly IProcessor<UserRolePermissionBaseModel> _IProcessor = null;
        public UserRolePermissionController (IProcessor<UserRolePermissionBaseModel> IProcessor) {
            _IProcessor = IProcessor;
        }

        ///<summary>
        ///Get All Record
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetUserRolePermission (Guid _MenuId) {
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
        [Route ("GetUserRolePermissionById")]
        public async Task<IActionResult> GetUserRolePermissionById (Guid _MenuId,[FromHeader] Guid _Id) {
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
        public async Task<IActionResult> AddUserRolePermission ([FromBody] UserRolePermissionAddModel model) {
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
        public async Task<IActionResult> UpdateUserRolePermission ([FromBody] UserRolePermissionUpdateModel model) {
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
        public async Task<IActionResult> DeleteUserRolePermission ([FromBody] ClaimDeleteModel model) {
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