using System;

namespace DentalCenterAPI.Models.News.Basic
{
    public class NewsBasicModel
    {
        public Guid NewsID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public string Discription { get; set; }
        public string Writter { get; set; }
        public string FirstDetails { get; set; }
        public string SecondDetails { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}