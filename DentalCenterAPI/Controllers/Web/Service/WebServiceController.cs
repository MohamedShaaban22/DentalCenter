using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.DentalCenterAPI.Controllers.Web.Service
{
    [ApiController]
    public class WebServiceController : ControllerBase
    {
        private IServiceService _service;
        public WebServiceController(IServiceService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All services Ordered by name
        /// </summary>
        /// <returns>Return List of service Details</returns>
        [HttpGet("api/v1/service")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync("", true);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get service By ID
        /// </summary>
        /// <param name="serviceid"></param>
        /// <returns>Return service Details</returns>
        [HttpGet("api/v1/service/{serviceid}")]
        public async Task<ActionResult> GetByID(Guid serviceid)
        {
            try
            {
                var result = await _service.GetByIDAsync(serviceid);
                if (result == null)
                    return BadRequest("Failed to get service");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get service By name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return service Details</returns>
        [HttpGet("api/v1/service/servicename")]
        public async Task<ActionResult> GetByname(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                    return BadRequest("Must Enter Service Name");
                var result = await _service.GetByNameAsync(name);
                if (result == null)
                    return BadRequest("Failed to get service");
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