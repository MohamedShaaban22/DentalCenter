using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.ServiceImage.Basic;
using DentalCenterAPI.Services.ServiceImage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.ServiceImage
{
    [Authorize]
    [ApiController]
    public class AdminServiceImageController : ControllerBase
    {
        private IServiceImageService _service;
        public AdminServiceImageController(IServiceImageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Gallery Images
        /// </summary>
        /// <param name="servicename"></param>
        /// <param name="sortby">Date Asc, Desc</param>
        /// <returns>Return List of Gallery Images</returns>
        [HttpGet("admin/v1/service/{servicename}/images")]
        public async Task<ActionResult> GetAll(string servicename, string sortby = "desc")
        {
            try
            {
                var result = await _service.GetAllAsync(servicename, sortby);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add List of ServiceImages by ServiceName 
        /// </summary>
        /// <param name="servicename"></param>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        [HttpPost("admin/v1/service/{servicename}/images")]
        public async Task<IActionResult> Add(string servicename, [FromBody] IEnumerable<ServiceImageBasicModel> model)
        {
            try
            {
                var result = await _service.AddAsync(servicename, model);
                if (result < 0)
                    return BadRequest("Failed to Add Service Images");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update ServiceImages ServiceName,Before, After Images Path 
        /// </summary>
        /// <param name="servicename"></param>
        /// <param name="serviceimageid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpPut("admin/v1/service/{servicename}/image/{serviceimageid}")]
        public async Task<IActionResult> Update(string servicename, Guid serviceimageid, [FromBody] ServiceImageBasicModel model)
        {
            try
            {
                //Check serviceImage Existance 
                var isExist = await _service.IsExistAsync(serviceimageid);
                if (isExist == null || isExist == false)
                    return BadRequest("serviceImage Not Exist");

                model.ServiceName = servicename.ToLower();
                model.ServiceImagesID = serviceimageid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update Service Image");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete List of ServiceImages By ServiceImagesIDs (For Admin)
        /// </summary>
        /// <param name="serviceImagesIDs"></param>
        /// <returns>Return Deleted Rows Count</returns>
        [HttpDelete("admin/v1/service/image")]
        public async Task<IActionResult> Delete(IEnumerable<ServiceImageBasicModel> serviceImagesIDs)
        {
            try
            {
                var result = await _service.DeleteAsync(serviceImagesIDs);
                if (result <= 0)
                    return BadRequest("Failed to Delete ServiceImages List");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Gallery Images By ID
        /// </summary>
        /// <param name="serviceimageid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/service/image/{serviceimageid}")]
        public async Task<IActionResult> Delete(Guid serviceimageid)
        {
            try
            {
                var result = await _service.DeleteAsync(new List<ServiceImageBasicModel>() { new ServiceImageBasicModel() { ServiceImagesID = serviceimageid } });
                if (result <= 0)
                    return BadRequest("Failed to Delete Service Image");

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