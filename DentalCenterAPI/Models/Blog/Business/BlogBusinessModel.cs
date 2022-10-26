using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.BlogDetails.Business;
using System.Collections.Generic;

namespace DentalCenterAPI.Models.Blog.Business
{
    public class BlogBusinessModel : BlogBasicModel
    {
        public IEnumerable<BlogDetailsBusinessModel> BlogDetails { get; set; }
        public string Date { get; set; }
    }
}