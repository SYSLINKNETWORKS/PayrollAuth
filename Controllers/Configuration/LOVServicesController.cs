using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TWP_API_Auth.Repository;

namespace TWP_API_Auth.Controllers {
    [Route ("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion ("1.0")]
    [Authorize]

    public class LOVServicesController : ControllerBase {
        private readonly ICfLovServicesRepository ICfLovServicesRepository = null;
        public LOVServicesController (ICfLovServicesRepository _ICfLovServicesRepository) {
            ICfLovServicesRepository = _ICfLovServicesRepository;
        }

        //Fetch Company Start
        [HttpGet]
        [Route ("GetCompany")]
        public async Task<IActionResult> GetCompany (String _srch) {
            var result = await ICfLovServicesRepository.GetComppaniesLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Fetch Branch End

        //Fetch Branch Start
        [HttpGet]
        [Route ("GetBranch")]
        public async Task<IActionResult> GetBranch (String _srch) {
            var result = await ICfLovServicesRepository.GetBranchesLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Fetch Branch End

        //Fetch Branch Start
        [HttpGet]
        [Route ("GetFinancialYear")]
        public async Task<IActionResult> GetFinancialYear (String _srch) {
            var result = await ICfLovServicesRepository.GetFinancialYearsLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Fetch Branch End

        //Menu Module Start
        [HttpGet]
        [Route ("GetMenuModule")]
        public async Task<IActionResult> GetModule (String _srch) {
            var result = await ICfLovServicesRepository.GetModuleLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //User Roles
        [HttpGet]
        [Route ("GetRoles")]
        public async Task<IActionResult> GetRoles (String _srch) {
            var result = await ICfLovServicesRepository.GetRolesLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }

        //Menu Category Start
        [HttpGet]
        [Route ("GetMenuCategories")]
        public async Task<IActionResult> GetMenuCategories (String _srch) {
            var result = await ICfLovServicesRepository.GetCategoriesLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }

        //Menu SubCategory Start
        [HttpGet]
        [Route ("GetMenuSubCategory")]
        public async Task<IActionResult> GetMenuSubCategory (String _srch) {
            var result = await ICfLovServicesRepository.GetSubCategoryLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }

        //Fetch Users Start
        [HttpGet]
        [Route ("GetUsers")]
        public async Task<IActionResult> GetUsers (String _srch) {
            var result = await ICfLovServicesRepository.GetUsersLovAsync (User, _srch);
            if (result == null) {
                return NotFound ();
            }
            return Ok (result);
        }
        //Fetch Users End

         //Fetch MenuPermissionByMenuId
        [HttpGet]
        [Route("GetMenuPermissionByMenuId")]
        public async Task<IActionResult> GetMenuPermissionByMenuId([FromHeader] Guid MenuId)
        {
            var result = await ICfLovServicesRepository.GetMenuPermissionByMenuIdLovAsync(User, MenuId);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //Fetch Users End

           //Fetch DashboardPermissionByName
        [HttpGet]
        [Route("GetDashboardPermissionByName")]
        public async Task<IActionResult> GetDashboardPermissionByName([FromHeader]  String MenuName)
        {
            var result = await ICfLovServicesRepository.GetDashboardPermissionByNameLovAsync(User, MenuName);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //Fetch Users End
    }
}