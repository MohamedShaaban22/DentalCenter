using System;

namespace DentalCenterAPI.Models.HappyPatient.Basic
{
    public class HappyPatientBasicModel
    {
        public Guid HappyPatientID { get; set; }
        public string VideoPath { get; set; }
        public bool? IsFavorite { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string Comment { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}