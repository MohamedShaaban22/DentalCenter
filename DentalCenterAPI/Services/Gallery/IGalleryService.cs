using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Gallery.Basic;

namespace DentalCenterAPI.Services.Gallery
{
    public interface IGalleryService
    {
        /// <summary>
        /// Get List of Gallery with search by Type, Sort By Date Asc, Desc(For Admin, Web)
        /// </summary>
        /// <param name="search">By Type</param>
        /// <param name="sortBy">ASC, Desc By Date</param>
        /// <returns>Return List of Gallery</returns>
        Task<IEnumerable<GalleryBasicModel>> GetAllAsync(string search, string sortBy);

        /// <summary>
        /// Check Gallery Existance (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid galleryID);

        /// <summary>
        /// Add List of Gallery Images by type (For Admin)
        /// </summary>
        /// <param name="type"></param>
        /// <param name="gallerys"></param>
        /// <returns>Return Added Rows Count</returns>
        Task<int> AddAsync(string type, IEnumerable<GalleryBasicModel> gallerys);

        /// <summary>
        /// Update Gallery Image Type (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <param name="type"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(Guid galleryID, string type);

        /// <summary>
        /// Update Gallery Image isFavorite (For Admin)
        /// </summary>
        /// <param name="galleryID"></param>
        /// <param name="type"></param>
        /// <param name="isFavorite"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateIsFavoriteAsync(Guid galleryID, string type, bool isFavorite);

        /// <summary>
        /// Delete List of Gallery  (For Admin)
        /// </summary>
        /// <param name="gallerys"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(IEnumerable<GalleryBasicModel> gallerys);
    }
}