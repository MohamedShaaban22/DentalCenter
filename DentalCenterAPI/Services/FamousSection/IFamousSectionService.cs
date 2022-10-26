using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.FamousSection.Basic;
using DentalCenterAPI.Models.FamousSection.Business;

namespace DentalCenterAPI.Services.FamousSection
{
    public interface IFamousSectionService
    {
        /// <summary>
        /// Get All FamousSection Ordered by Name (For Web, admin)
        /// </summary>
        /// <param name="search">name</param>
        /// <returns>Return List of FamousSection Details</returns>
        Task<IEnumerable<FamousSectionBasicModel>> GetAllAsync(string search);

        /// <summary>
        /// Get FamousSection Details by ID with list of FamousSection images (For Web, Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return FamousSection Details with list of Images</returns>
        Task<FamousSectionBusinessModel> GetByIDAsync(Guid famousSectionID);

        /// <summary>
        /// Check FamousSection Existance (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid famousSectionID);

        /// <summary>
        /// Add FamousSection with list of FamousSection Images (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Added FamousSectionID</returns>
        Task<Guid?> AddAsync(FamousSectionBusinessModel model);

        /// <summary>
        /// Update FamousSection Details with list of FamousSectionImages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(FamousSectionBusinessModel model);

        /// <summary>
        /// Delete FamousSection with list of FamousSection images (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid famousSectionID);
    }
}