using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.News;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.DentalCenterAPI_Controllers_Web_News
{
    [ApiController]
    public class WebNewsController : ControllerBase
    {
        private INewsService _service;
        public WebNewsController(INewsService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All News Ordered by Date desc 
        /// </summary>
        /// <returns>Return List of News Details</returns>
        [HttpGet("api/v1/news")]
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
        /// Get News Details by ID with list of news images
        /// </summary>
        /// <param name="newsid"></param>
        /// <returns>Return News Details with list of Images</returns>
        [HttpGet("api/v1/news/{newsid}")]
        public async Task<ActionResult> GetByID(Guid newsid)
        {
            try
            {
                var result = await _service.GetByIDAsync(newsid);
                if (result == null)
                    return BadRequest("Failed to Get News Details");
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