using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.Branch;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Branch
{
    [ApiController]
    public class WebBranchController : ControllerBase
    {
        private IBranchService _service;
        public WebBranchController(IBranchService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Branches for Dropdown
        /// </summary>
        /// <returns>Return List of Branches Dropdown That Contains (ID And Name)</returns>
        [HttpGet("api/v1/dropdown/branch")]
        public async Task<ActionResult> GetAllForDropDown()
        {
            try
            {
                var result = await _service.GetAllForDropdownAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get List of Branches
        /// </summary>
        /// <returns>Return List of Branches</returns>
        [HttpGet("api/v1/branch")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }
    }
}