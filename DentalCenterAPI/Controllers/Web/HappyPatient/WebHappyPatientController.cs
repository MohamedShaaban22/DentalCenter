using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.HappyPatient;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.HappyPatient
{
    [ApiController]
    public class WebHappyPatientController : ControllerBase
    {

        private IHappyPatientService _service;
        public WebHappyPatientController(IHappyPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Happy Patients With Two Lists (Favorite, All Videos) Ordered by Date, Favorite
        /// </summary>
        /// <param name="type">video, review</param>
        /// <param name="isfavorite">for review only</param>
        /// <returns>Return Two Lists of Happy Paitents Favorite, Normal</returns>
        [HttpGet("api/v1/happypatientstype/{type}/happypatients")]
        public async Task<ActionResult> GetAll(string type, bool? isfavorite)
        {
            try
            {
                var result = await _service.GetAllAsync(type, isfavorite);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Random Happy Patient By Type
        /// </summary>
        /// <param name="type">video, review</param>
        /// <returns>Return Updated Rows Count</returns>
        [HttpGet("api/v1/happypatientstype/{type}/happypatient/random")]
        public async Task<IActionResult> GetRandomOne(string type)
        {
            try
            {
                var result = await _service.GetRendomBTypeAsync(type);
                if (result == null)
                    return BadRequest("Failed to Get Happy Patient");

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