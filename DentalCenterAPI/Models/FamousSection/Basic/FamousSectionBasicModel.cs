using System;

namespace DentalCenterAPI.Models.FamousSection.Basic
{
    public class FamousSectionBasicModel
    {
        public Guid FamousSectionID { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public DateTime InsertDate { get; set; }
    }
}