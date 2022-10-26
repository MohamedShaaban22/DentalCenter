using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.FamousSection;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.FamousSection
{
    [ApiController]
    public class WebFamousSectionController : ControllerBase
    {
        private IFamousSectionService _service;
        public WebFamousSectionController(IFamousSectionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All FamousSection Ordered by Name
        /// </summary>
        /// <returns>Return List of FamousSection Details</returns>

        [HttpGet("api/v1/famoussection")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync("");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get FamousSection Details by ID with list of FamousSection images
        /// </summary>
        /// <param name="famousSectionid"></param>
        /// <returns>Return FamousSection Details with list of Images</returns>

        [HttpGet("api/v1/famoussection/{famousSectionid}")]
        public async Task<ActionResult> GetByID(Guid famousSectionid)
        {
            try
            {
                var result = await _service.GetByIDAsync(famousSectionid);
                if (result == null)
                    return BadRequest("Failed to Get famous section Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }
    }
}