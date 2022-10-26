using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Subscribtion.Basic;
using DentalCenterAPI.Services.Subscribtion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Subscribtion
{
    [Authorize]
    [ApiController]
    public class AdminSubscribtionController : ControllerBase
    {
        private ISubscribtionService _service;
        public AdminSubscribtionController(ISubscribtionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Subscribtion
        /// </summary>
        /// <returns>Return List of Subscribtion</returns>
        [HttpGet("admin/v1/subscribtion")]
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

        /// <summary>
        /// Add Subscribtion
        /// </summary>
        /// <param name="subscribtion"></param>
        /// <returns>Return Added Subscribtion GUID</returns>
        [HttpPost("admin/v1/subscribtion")]
        public async Task<IActionResult> Add([FromBody] SubscribtionBasicModel subscribtion)
        {
            try
            {
                //Check Email Existance 
                var isExist = await _service.IsEmailExistAsync(subscribtion.Email);
                if (isExist != null && isExist == true)
                    return Ok(1);

                var result = await _service.AddAsync(subscribtion);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add Subscribtion");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Subscribtion
        /// </summary>
        /// <param name="subscribtionid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/subscribtion/{subscribtionid}")]
        public async Task<IActionResult> Delete(Guid subscribtionid)
        {
            try
            {
                //Check subscribtion Existance 
                var isExist = await _service.IsExistAsync(subscribtionid);
                if (isExist == null || isExist == false)
                    return BadRequest("Subscribtion Not Exist");

                var result = await _service.DeleteAsync(subscribtionid);
                if (result <= 0)
                    return BadRequest("Failed to Delete Subscribtion");

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