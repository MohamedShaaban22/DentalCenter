using System;

namespace DentalCenterAPI.Models.Appointment.Business
{
    public class AppointmentAddBusinessModel
    {
        public string ServiceName { get; set; }
        public string BranchName { get; set; }
        public string Type { get; set; }
        public string NumberOfPatients { get; set; }
        public bool LastService { get; set; }
        public bool FlightTickets { get; set; }
        public bool Tour { get; set; }
        public bool DomescticTransportation { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ReservationTime { get; set; }
        public DateTime? InsertDate { get; set; }

        //Patient
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ContactMethod { get; set; }
        public DateTime BirthDate { get; set; }
        public string FirstKnowUsFrom { get; set; }
        public string SecondKnowUsFrom { get; set; }
        public string PatientStatus { get; set; }
        public string Country { get; set; }
        public string Nationality { get; set; }
        public bool IsResident { get; set; }
    }
}