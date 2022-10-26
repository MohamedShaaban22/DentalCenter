using System;

namespace DentalCenterAPI.Models.Patient.Basic
{
    public class PatientBasicModel
    {
        public Guid PatientID { get; set; }
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
        public DateTime? InsertDate { get; set; }
    }
}