using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.ServiceImage.Basic;

namespace DentalCenterAPI.Services.ServiceImage
{
    public interface IServiceImageService
    {
        /// <summary>
        /// Get List of ServiceImages with search by ServiceName, Sort By Date Asc, Desc(For Admin, Web)
        /// </summary>
        /// <param name="search">By serviceName</param>
        /// <param name="sortBy">ASC, Desc By Date</param>
        /// <returns>Return List of ServiceImages</returns>
        Task<IEnumerable<ServiceImageBasicModel>> GetAllAsync(string search, string sortBy);

        /// <summary>
        /// Check ServiceImage Existance By ServiceImagesID (For Admin)
        /// </summary>
        /// <param name="serviceImageID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid serviceImageID);

        /// <summary>
        /// Add List of ServiceImages by ServiceName (For Admin)
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="serviceImages"></param>
        /// <returns>Return Added Rows Count</returns>
        Task<int> AddAsync(string serviceName, IEnumerable<ServiceImageBasicModel> serviceImages);

        /// <summary>
        /// Update ServiceImages ServiceName,Before, After Images Path (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(ServiceImageBasicModel model);

        /// <summary>
        /// Delete List of ServiceImages By ServiceImagesIDs(For Admin)
        /// </summary>
        /// <param name="ServiceImages"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(IEnumerable<ServiceImageBasicModel> ServiceImages);
    }
}