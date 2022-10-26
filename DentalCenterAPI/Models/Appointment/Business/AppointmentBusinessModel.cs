using DentalCenterAPI.Models.Appointment.Basic;
using DentalCenterAPI.Models.Patient.Basic;

namespace DentalCenterAPI.Models.Appointment.Business
{
    public class AppointmentBusinessModel : AppointmentBasicModel
    {
        public PatientBasicModel Patient { get; set; }
    }
}