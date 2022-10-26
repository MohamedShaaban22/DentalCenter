using DentalCenterAPI.Models.NewsImages.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalCenterAPI.Services.NewsImage
{
    public interface INewsImageService
    {
        /// <summary>
        /// Update newsImage by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> AddAndDeleteAsync(Guid newsID, IEnumerable<NewsImagesBusinessModel> model);
    }
}