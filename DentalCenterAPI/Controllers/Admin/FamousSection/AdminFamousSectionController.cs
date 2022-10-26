using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.FamousSection.Business;
using DentalCenterAPI.Services.FamousSection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.FamousSection
{
    [Authorize]
    [ApiController]
    public class AdminFamousSectionController : ControllerBase
    {
        private IFamousSectionService _service;
        public AdminFamousSectionController(IFamousSectionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All FamousSection Ordered by Name
        /// </summary>
        /// <param name="search">name</param>
        /// <returns>Return List of FamousSection Details</returns>

        [HttpGet("admin/v1/famoussection")]
        public async Task<ActionResult> GetAll(string search = "")
        {
            try
            {
                var result = await _service.GetAllAsync(search);
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

        [HttpGet("admin/v1/famoussection/{famousSectionid}")]
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

        /// <summary>
        /// Add FamousSection with list of FamousSection Images
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added FamousSectionID</returns>

        [HttpPost("admin/v1/famoussection")]
        public async Task<ActionResult> Add([FromBody] FamousSectionBusinessModel model)
        {
            try
            {
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add famous section Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Update FamousSection with list of FamousSection Images
        /// </summary>
        /// <param name="famousSectionid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>

        [HttpPut("admin/v1/famoussection/{famousSectionid}")]
        public async Task<ActionResult> Update(Guid famousSectionid, [FromBody] FamousSectionBusinessModel model)
        {
            try
            {
                model.FamousSectionID = famousSectionid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update famous section Details");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete FamousSection with list of FamousSection images (For Admin)
        /// </summary>
        /// <param name="famousSectionid"></param>
        /// <returns>Return Deleted Rows Count</returns>
        [HttpDelete("admin/v1/famoussection/{famousSectionid}")]
        public async Task<ActionResult> Delete(Guid famousSectionid)
        {
            try
            {
                var result = await _service.DeleteAsync(famousSectionid);
                if (result <= 0)
                    return BadRequest("Failed to Delete famous section Details");
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