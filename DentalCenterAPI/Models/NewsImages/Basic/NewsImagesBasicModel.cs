using System;

namespace DentalCenterAPI.Models.NewsImages.Basic
{
    public class NewsImagesBasicModel
    {
        public Guid ImageID { get; set; }
        public string ImagePath { get; set; }
        public Guid? NewsID { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}