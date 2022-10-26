using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Subscribtion.Basic;
using DentalCenterAPI.Services.Subscribtion;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Subscribtion
{
    [ApiController]
    public class WebSubscribtionController : ControllerBase
    {
        private ISubscribtionService _service;
        public WebSubscribtionController(ISubscribtionService service)
        {
            _service = service;
        }

        /// <summary>
        /// Add Subscribtion
        /// </summary>
        /// <param name="subscribtion"></param>
        /// <returns>Return Added Subscribtion GUID</returns>
        [HttpPost("api/v1/subscribtion")]
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
    }
}