using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.HomePage;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.HomePage
{
    [ApiController]
    public class WebHomePageController : ControllerBase
    {
        private IHomePageService _service;
        public WebHomePageController(IHomePageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Home Page (Counters, Favorite Gallery, Home Doctors, Favorite HappyPatients Revirews) (For Web)
        /// </summary>
        /// <returns>Return Home Page Details</returns>
        [HttpGet("api/v1/homepage")]
        public async Task<ActionResult> GetAll(string type, bool? isfavorite)
        {
            try
            {
                var result = await _service.GetAllAsync();
                if (result == null)
                    return BadRequest("Faild to get Home Page");
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