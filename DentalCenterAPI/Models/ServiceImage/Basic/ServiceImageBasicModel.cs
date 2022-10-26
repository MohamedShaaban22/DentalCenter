using System;

namespace DentalCenterAPI.Models.ServiceImage.Basic
{
    public class ServiceImageBasicModel
    {
        public Guid ServiceImagesID { get; set; }
        public string ServiceName { get; set; }
        public string BeforeImagePath { get; set; }
        public string AfterImagePath { get; set; }
        public string Description { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}