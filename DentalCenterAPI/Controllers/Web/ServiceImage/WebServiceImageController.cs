using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.ServiceImage;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.ServiceImage
{
    [ApiController]
    public class WebServiceImageController : ControllerBase
    {
        private IServiceImageService _service;
        public WebServiceImageController(IServiceImageService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get List of Gallery Images
        /// </summary>
        /// <param name="servicename"></param>
        /// <param name="sortby">Date Asc, Desc</param>
        /// <returns>Return List of Gallery Images</returns>
        [HttpGet("api/v1/service/{servicename}/images")]
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
    }
}