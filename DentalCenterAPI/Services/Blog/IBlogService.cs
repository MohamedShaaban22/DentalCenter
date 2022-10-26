using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Blog.Business;

namespace DentalCenterAPI.Services.Blog
{
    public interface IBlogService
    {
        /// <summary>
        /// Get List of Blogs (For Admin, Web)
        /// </summary>
        /// <returns>Return List of Blogs</returns>
        Task<IEnumerable<BlogBasicModel>> GetAllAsync();

        /// <summary>
        /// Get Blog Details By BlogsID (For Admin, Web)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return Blog Details</returns>
        Task<BlogBusinessModel> GetByIDAsync(Guid blogID);

        /// <summary>
        /// Check Blog Existance (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid blogID);

        /// <summary>
        /// Add Blog (For Admin)
        /// </summary>
        /// <param name="blog"></param>
        /// <returns>Return Added Blog GUID</returns>
        Task<Guid?> AddAsync(BlogBusinessModel blog);

        /// <summary>
        /// Update Blog (For Admin)
        /// </summary>
        /// <param name="branch"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(BlogBusinessModel branch);

        /// <summary>
        /// Delete Blog (For Admin)
        /// </summary>
        /// <param name="blogID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid blogID);
    }
}