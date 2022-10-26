using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DentalCenterAPI.Models.Service.Basic;

namespace DentalCenterAPI.Services.Service
{
    public interface IServiceService
    {
        /// <summary>
        /// Get List of Services with search
        /// </summary>
        /// <param name="search">name, info</param>
        /// <param name="sortByAsc">name = True, false</param>
        /// <returns>Return List of Services</returns>
        Task<IEnumerable<ServiceBasicModel>> GetAllAsync(string search, bool sortByAsc);


        /// <summary>
        /// Get of Service By ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return Service Details</returns>
        Task<ServiceBasicModel> GetByIDAsync(Guid serviceID);

        /// <summary>
        /// Get of Service By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return Service Details</returns>
        Task<ServiceBasicModel> GetByNameAsync(string name);


        /// <summary>
        /// Check Service Existance By ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return True or False</returns>
        Task<bool?> IsExistByIDAsync(Guid serviceID);

        /// <summary>
        /// Check Service Existance By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return True or False</returns>
        Task<bool?> IsExistByNameAsync(string name);

        /// <summary>
        /// Check Service Existance By Name, And exclude Current ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="name"></param>
        /// <returns>Return True or False</returns>
        Task<bool?> IsExistByNameAsync(Guid serviceID, string name);

        /// <summary>
        /// Add Service 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Return Added Row GUID</returns>
        Task<Guid?> AddAsync(ServiceBasicModel entity);

        /// <summary>
        /// Update Service 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(ServiceBasicModel model);

        /// <summary>
        /// Delete Service 
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        Task<int> DeleteAsync(Guid serviceID);
    }
}