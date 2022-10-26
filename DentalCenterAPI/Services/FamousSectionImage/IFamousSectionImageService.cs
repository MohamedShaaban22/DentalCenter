using DentalCenterAPI.Models.FamousSection.Business;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DentalCenterAPI.Services.FamousSectionImage
{
    public interface IFamousSectionImageService
    {
        /// <summary>
        /// Update FamousSectionImages by delete list then add it again  (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> AddAndDeleteAsync(Guid famousSectionID, IEnumerable<FamousSectionImageBusinessModel> model);

        /// <summary>
        /// Delete List By famousSectionID (For Admin)
        /// </summary>
        /// <param name="famousSectionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteByFamousSectionIDAsync(Guid famousSectionID);

        /// <summary>
        /// Delete By famousSectionImageID (For Admin)
        /// </summary>
        /// <param name="famousSectionImageID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteByFamousSectionImageIDAsync(Guid famousSectionImageID);
    }
}