using System;

namespace DentalCenterAPI.Models.Blog.Basic
{
    public class BlogBasicModel
    {
        public Guid BlogID { get; set; }
        public string Title { get; set; }
        public string Writter { get; set; }
        public string ImagePath { get; set; }
        public Guid DoctorID { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}