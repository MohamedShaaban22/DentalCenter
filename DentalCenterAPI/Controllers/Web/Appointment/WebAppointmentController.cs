using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Appointment.Business;
using DentalCenterAPI.Services.Appointment;
using Microsoft.AspNetCore.Mvc;

namespace DentalCenterAPI.Controllers.Web.Appointment
{
    [ApiController]
    public class WebAppointmentController : ControllerBase
    {
        private IAppointmentService _service;
        public WebAppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get Appointment With Patient Details By appointmentID
        /// </summary>
        /// <param name="type">tourism, exist, new</param>
        /// <param name="model"></param>
        /// <returns>Return Appointment Details</returns>
        [HttpPost("api/v1/appointmenttpe/{type}/appointment")]
        public async Task<ActionResult> Add(string type, [FromBody] AppointmentAddBusinessModel model)
        {
            try
            {
                model.Type = type.ToLower();
                var result = await _service.AddAsync(model);
                if (result == null || result == Guid.Empty)
                    return BadRequest("Failed to add appointment");
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