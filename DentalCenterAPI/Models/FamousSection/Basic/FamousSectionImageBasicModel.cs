using System;

namespace DentalCenterAPI.Models.FamousSection.Basic
{
    public class FamousSectionImageBasicModel
    {
        public Guid FamousSectionImageID { get; set; }
        public Guid FamousSectionID { get; set; }
        public string ImagePath { get; set; }
        public DateTime InsertDate { get; set; }
    }
}