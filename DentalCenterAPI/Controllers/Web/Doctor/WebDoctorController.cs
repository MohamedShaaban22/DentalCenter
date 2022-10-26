using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.Doctor;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Doctor
{
    [ApiController]
    public class WebDoctorController : ControllerBase
    {
        private IDoctorService _service;
        public WebDoctorController(IDoctorService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Doctors (Name,ID, Image) For for homepage (For Web)
        /// </summary>
        /// <returns>Return List of Doctors(Name,ID, Image)</returns>
        [HttpGet("api/v1/doctor/explore")]
        public async Task<ActionResult> GetAllExplore()
        {
            try
            {
                var result = await _service.GetAllForSliderAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get All Doctors 
        /// </summary>
        /// <returns>Return List of Doctors</returns>
        [HttpGet("api/v1/doctor")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                var result = await _service.GetAllAsync(true, "name", "");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get Doctor With Patients, Blogs By Doctor ID (for Admin, Web)
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns>Return Doctor Details with Patients, Blogs </returns>
        [HttpGet("api/v1/doctor/{doctorid}")]
        public async Task<ActionResult> GetByyID(Guid doctorid)
        {
            try
            {
                var result = await _service.GetByIDAsync(doctorid);
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