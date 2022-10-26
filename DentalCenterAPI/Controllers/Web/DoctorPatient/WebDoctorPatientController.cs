using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.DoctorPatient;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.DoctorPatient
{
    [ApiController]
    public class WebDoctorPatientController : ControllerBase
    {
        private IDoctorPatientService _service;
        public WebDoctorPatientController(IDoctorPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Doctors Patients By DoctorID
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns>Return List of Doctor Patients</returns>
        [HttpGet("api/v1/doctorpatient/doctor/{doctorid}")]
        public async Task<ActionResult> GetByID(Guid doctorid)
        {
            try
            {
                var result = await _service.GetAllAsync(doctorid);
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