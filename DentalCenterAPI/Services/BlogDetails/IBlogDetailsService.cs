using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.BlogDetails.Business;

namespace DentalCenterAPI.Services.BlogDetails
{
    public interface IBlogDetailsService
    {
        /// <summary>
        /// Add Blog Details (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <param name="model"></param>
        /// <returns>Return Added Rows Count</returns>
        Task<int> AddAsync(Guid blogID, IEnumerable<BlogDetailsBusinessModel> model);

        /// <summary>
        /// Update Blog Details (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(Guid blogID, IEnumerable<BlogDetailsBusinessModel> model);

    }
}