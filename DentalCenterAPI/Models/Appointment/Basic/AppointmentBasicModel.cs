using System;

namespace DentalCenterAPI.Models.Appointment.Basic
{
    public class AppointmentBasicModel
    {
        public Guid AppointmentID { get; set; }
        public string ServiceName { get; set; }
        public string BranchName { get; set; }
        public Guid PatientID { get; set; }
        public string Type { get; set; }
        public bool LastService { get; set; }
        public string NumberOfPatients { get; set; }
        public bool FlightTickets { get; set; }
        public bool Tour { get; set; }
        public bool DomescticTransportation { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ReservationTime { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}