using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Services.Appointment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Admin.Appointment
{
    [Authorize]
    [ApiController]
    public class AdminAppointmentController : ControllerBase
    {
        private IAppointmentService _service;
        public AdminAppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get All Appointments With Patient Details By Type, IsNew With Search
        /// </summary>
        /// <param name="type">tourism, exist, new</param>
        /// <param name="search">BranchName, ServiceName, PhoneNumber, PatientName</param>
        /// <returns>Return List of Appointments</returns>
        [HttpGet("admin/v1/appointmenttpe/{type}/appointment")]
        public async Task<ActionResult> GetAll(string type, string search = "")
        {
            try
            {
                var result = await _service.GetAllByTypeAsync(type, search);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Appointment With Patient Details By appointmentID
        /// </summary>
        /// <param name="appointmentid"></param>
        /// <returns>Return Appointment Details</returns>
        [HttpGet("admin/v1/appointment/{appointmentid}")]
        public async Task<ActionResult> GetByID(Guid appointmentid)
        {
            try
            {
                var result = await _service.GetByIDAsync(appointmentid);
                if (result == null)
                    return BadRequest("Failed to Get appointment");
                return Ok(result);
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// Get Appointment With Patient Details By appointmentID
        /// </summary>
        /// <param name="appointmentid"></param>
        /// <returns>Return Appointment Details</returns>
        [HttpDelete("admin/v1/appointment/{appointmentid}")]
        public async Task<ActionResult> Delete(Guid appointmentid)
        {
            try
            {
                var result = await _service.DeleteAsync(appointmentid);
                if (result <= 0)
                    return BadRequest("Failed to Delete appointment");
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