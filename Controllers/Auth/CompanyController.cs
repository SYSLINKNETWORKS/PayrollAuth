using System;
using System.Threading.Tasks;
using TWP_API_Auth.Generic;
using TWP_API_Auth.Processor;
using TWP_API_Auth.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TWP_API_Auth.Controllers {
    ///<summary>
    ///Menu
    ///</summary>

    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]
    public class CompanyController : ControllerBase {
        private readonly IProcessor<CompanyBaseModel> _IProcessor = null;
        public CompanyController (IProcessor<CompanyBaseModel> IProcessor) {
            _IProcessor = IProcessor;
        }

        ///<summary>
        ///Get All Record
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetCompany ( [FromHeader] Guid _MenuId) {
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

        ///<summary>
        ///Get Record by id
        ///</summary>
        [HttpGet]
        [Route ("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById ([FromHeader] Guid _MenuId,[FromHeader] Guid _Id) {
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
 
        ///<summary>
        ///Save Record
        ///</summary>
        [HttpPost]
        public async Task<IActionResult> AddCompany ([FromBody] CompanyAddModel model) {
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

        ///<summary>
        ///Update Record
        ///</summary>
        [HttpPut]
        public async Task<IActionResult> UpdateCompany ([FromBody] CompanyUpdateModel model) {
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
        public async Task<IActionResult> DeleteCompany ([FromBody] CompanyDeleteModel model) {
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