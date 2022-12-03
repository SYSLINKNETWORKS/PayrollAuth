using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Auth.Processor;
using TWP_API_Auth.Repository;
using TWP_API_Auth.ViewModels;

namespace TWP_API_Auth.Controllers {
    ///<summary>
    ///Menu
    ///</summary>

    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]
    public class GetMenuController : ControllerBase {
        private readonly ICfLovServicesRepository ICfLovServicesRepository = null;
        public GetMenuController (ICfLovServicesRepository _ICfLovServicesRepository) {
            ICfLovServicesRepository = _ICfLovServicesRepository;
        }

        ///<summary>
        ///Get Menu User Wise
        ///</summary>
        [HttpGet]
        public async Task<IActionResult> GetMenu () {
            var result = await ICfLovServicesRepository.GetUserMenuLovAsync (User);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Get End

        ///<summary>
        ///Get Report Menu User Wise and Master Menu Wise
        ///</summary>
        [HttpGet]
        [Route ("GetReportMenu")]
        public async Task<IActionResult> GetReportMenu ([FromHeader] Guid _MenuId) {
            var result = await ICfLovServicesRepository.GetUserReportMenuLovAsync (User, _MenuId);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Get End

    }

}