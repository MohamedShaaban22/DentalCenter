using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Subscribtion.Basic;

namespace DentalCenterAPI.Services.Subscribtion
{
    public interface ISubscribtionService
    {
        /// <summary>
        /// Get List of Subscribtions (For Admin)
        /// </summary>
        /// <returns>Return List of Subscribtions</returns>
        Task<IEnumerable<SubscribtionBasicModel>> GetAllAsync();

        /// <summary>
        /// Check Subscribtion Existance (For Admin)
        /// </summary>
        /// <param name="subscribtionID"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsExistAsync(Guid subscribtionID);

        /// <summary>
        /// Check Subscribtion email Existance (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return (True, False)</returns>
        Task<bool?> IsEmailExistAsync(string email);

        /// <summary>
        /// Add Subscribtion  (For Admin, Web)
        /// </summary>
        /// <param name="subscribtion"></param>
        /// <returns>Return Added Subscribtion GUID</returns>
        Task<Guid?> AddAsync(SubscribtionBasicModel subscribtion);

        /// <summary>
        /// Delete Subscribtion (For Admin)
        /// </summary>
        /// <param name="subscribtionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid subscribtionID);
    }
}