using System;

namespace DentalCenterAPI.Models.DoctorPatient.Basic
{
    public class DoctorPatientBasicModel
    {
        public Guid DoctorPatientID { get; set; }
        public Guid DoctorID { get; set; }
        public string PatientImagePath { get; set; }
        public string PatientWithDoctorImagePath { get; set; }
        public string Caught { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}