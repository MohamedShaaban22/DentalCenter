using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.Gallery;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Gallery
{
    [ApiController]
    public class WebGalleryController : ControllerBase
    {
        private IGalleryService _service;
        public WebGalleryController(IGalleryService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Gallery Images
        /// </summary>
        /// <param name="search">By Type With Default value (All)</param>
        /// <param name="sortby">Date Asc, Desc</param>
        /// <returns>Return List of Gallery Images</returns>
        [HttpGet("api/v1/gallery")]
        public async Task<ActionResult> GetAll(string search = "all", string sortby = "desc")
        {
            try
            {
                var result = await _service.GetAllAsync(search, sortby);
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