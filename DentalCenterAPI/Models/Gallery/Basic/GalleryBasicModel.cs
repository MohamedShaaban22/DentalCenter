using System;

namespace DentalCenterAPI.Models.Gallery.Basic
{
    public class GalleryBasicModel
    {
        public Guid GalleryID { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public bool IsFavorite { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}