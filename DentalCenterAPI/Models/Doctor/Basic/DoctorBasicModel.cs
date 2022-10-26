using System;

namespace DentalCenterAPI.Models.Doctor.Basic
{
    public class DoctorBasicModel
    {
        public Guid DoctorID { get; set; }
        public string Name { get; set; }
        public string FirstDiscription { get; set; }
        public string SecondDiscription { get; set; }
        public string ThirdDiscription { get; set; }
        public string ReferalNumber { get; set; }
        public string HomeImagePath { get; set; }
        public string PersonalImagePath { get; set; }
        public string ExploreImagePath { get; set; }
        public string VideoPath { get; set; }
        public string TiktokURL { get; set; }
        public string FacebookURL { get; set; }
        public string InstgramURL { get; set; }
        public string LinkedInURL { get; set; }
        public bool? HomeDisplay { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}