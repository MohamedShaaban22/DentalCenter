using System;
using System.Collections.Generic;
using DentalCenterAPI.Models.News.Basic;
using DentalCenterAPI.Models.NewsImages.Business;

namespace DentalCenterAPI.Models.News.Business
{
    public class NewsBusinessModel : NewsBasicModel
    {
        public string Date { get; set; }
        public IEnumerable<NewsImagesBusinessModel> NewsImages { get; set; }
    }
}