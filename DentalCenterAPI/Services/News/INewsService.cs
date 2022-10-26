using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.News.Basic;
using DentalCenterAPI.Models.News.Business;

namespace DentalCenterAPI.Services.News
{
    public interface INewsService
    {
        /// <summary>
        /// Get All News Ordered by Date desc (For Web, admin)
        /// </summary>
        /// <returns>Return List of News Details</returns>
        Task<IEnumerable<HomeNewsBusinessModel>> GetAllAsync();

        /// <summary>
        /// Get News Details by ID with list of news images (For Web, Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return News Details with list of Images</returns>
        Task<NewsBusinessModel> GetByIDAsync(Guid newsID);

        /// <summary>
        /// Check News Existance (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid newsID);

        /// <summary>
        /// Add News with list of News Images (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added NewsID</returns>
        Task<Guid?> AddAsync(NewsBusinessModel model);

        /// <summary>
        /// Update news Details with list of newsimages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(NewsBusinessModel model);

        /// <summary>
        /// Delete news with list of news images (For Admin)
        /// </summary>
        /// <param name="newsID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid newsID);
    }
}