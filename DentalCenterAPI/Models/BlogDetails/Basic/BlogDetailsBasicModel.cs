using System;

namespace DentalCenterAPI.Models.BlogDetails.Basic
{
    public class BlogDetailsBasicModel
    {
        public Guid DetailsID { get; set; }
        public string Title { get; set; }
        public string Discreption { get; set; }
        public string ImagePath { get; set; }
        public int? DetailsOrder { get; set; }
        public Guid? BlogID { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}