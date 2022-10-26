using System;

namespace DentalCenterAPI.Models.BlogDetails.Business
{
    public class BlogDetailsBusinessModel
    {
        public Guid DetailsID { get; set; }
        public string Title { get; set; }
        public string Discreption { get; set; }
        public string ImagePath { get; set; }
        public int DetailsOrder { get; set; }
    }
}