using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.DoctorPatient.Basic;
using DentalCenterAPI.Services.DoctorPatient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.DoctorPatient
{
    [Authorize]
    [ApiController]
    public class AdminDoctorPatientController : ControllerBase
    {
        private IDoctorPatientService _service;
        public AdminDoctorPatientController(IDoctorPatientService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All DoctorsPatients 
        /// </summary>
        /// <param name="doctorid"></param>
        /// <returns>Return List of Doctor Patients</returns>
        [HttpGet("admin/v1/doctorpatient/doctor/{doctorid}")]
        public async Task<ActionResult> GetALLByDoctorID(Guid doctorid)
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

        /// <summary>
        /// Add DoctorPatient
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added Doctor Patient GUID</returns>
        [HttpPost("admin/v1/doctorpatient")]
        public async Task<IActionResult> Add([FromBody] DoctorPatientBasicModel model)
        {
            try
            {
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to Add doctorPatient");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Doctor Patient
        /// </summary>
        /// <param name="doctorpatientid"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated rows count</returns>
        [HttpPut("admin/v1/doctorpatient/{doctorpatientid}")]
        public async Task<IActionResult> Update(Guid doctorpatientid, [FromBody] DoctorPatientBasicModel model)
        {
            try
            {
                model.DoctorPatientID = doctorpatientid;
                var result = await _service.UpdateAsync(model);
                if (result <= 0)
                    return BadRequest("Failed to Update doctorPatient");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Doctor Patient
        /// </summary>
        /// <param name="doctorpatientid"></param>
        /// <returns>Return Deleted rows count</returns>
        [HttpDelete("admin/v1/doctorpatient/{doctorpatientid}")]
        public async Task<IActionResult> Delete(Guid doctorpatientid)
        {
            try
            {
                var result = await _service.DeleteAsync(doctorpatientid);
                if (result <= 0)
                    return BadRequest("Failed to Delete doctor patient");

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