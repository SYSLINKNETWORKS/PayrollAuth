using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Auth.Processor;
using TWP_API_Auth.Services;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Controllers {
    ///<summary>
    ///User Claims
    ///</summary>
    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]
    public class UserClaimController : ControllerBase {
        private readonly IProcessor<UserClaimsViewModel> _IProcessor;

        public UserClaimController (IProcessor<UserClaimsViewModel> IProcessor) {
            _IProcessor = IProcessor;
        }

        ///<summary>
        ///Get Record by Id
        ///</summary>
        [HttpGet ("GetUserClaimById")]
        public async Task<IActionResult> GetUserClaimById (Guid _MenuId, [FromHeader] Guid _Id) {
            try {
                var result = await _IProcessor.ProcessGetById (_Id, _MenuId, User);
                return Ok (result);

            } catch (Exception e) {
                string innerexp = "";
                if (e.InnerException != null) {
                    innerexp = " Inner Error : " + e.InnerException.ToString ();
                }
                return BadRequest (e.Message.ToString () + innerexp);
            }
        }

        ///<summary>
        ///Save Record
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> UpdateUserClaim ([FromBody] UserClaimsViewModel model) {
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

        ///<summary>
        ///Delete Record
        ///</summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteUserClaim ([FromBody] ClaimDeleteModel model) {
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

    }
}