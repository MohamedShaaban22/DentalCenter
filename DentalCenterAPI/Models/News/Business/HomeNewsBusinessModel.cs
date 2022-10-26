using System;

namespace DentalCenterAPI.Models.News.Business
{
    public class HomeNewsBusinessModel
    {
        public Guid NewsID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string ImagePath { get; set; }
        public string Discription { get; set; }
        public string Writter { get; set; }
        public string Date { get; set; }
    }
}