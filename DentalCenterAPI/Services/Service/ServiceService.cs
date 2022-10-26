using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Service.Basic;
using DentalCenterAPI.Repository.Service;

namespace DentalCenterAPI.Services.Service
{
    public class ServiceService : IServiceService
    {
        private ServiceRepository _repo;
        public ServiceService(ServiceRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get List of Services with search
        /// </summary>
        /// <param name="search">name, info</param>
        /// <param name="sortByAsc">name = True, false</param>
        /// <returns>Return List of Services</returns>
        public async Task<IEnumerable<ServiceBasicModel>> GetAllAsync(string search, bool sortByAsc)
        {
            try
            {
                var result = await _repo.GetAllAsync();
                if (!string.IsNullOrEmpty(search))
                    result = result.Where(x => x.Name?.ToLower().Contains(search) == true || x.Info?.ToLower().Contains(search) == true);
                result = (sortByAsc) ? result.OrderBy(x => x.Name) : result.OrderByDescending(x => x.Name);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get of Service By ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return Service Details</returns>
        public async Task<ServiceBasicModel> GetByIDAsync(Guid serviceID)
        {
            try
            {
                var result = await _repo.GetByIDAsync(serviceID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Get of Service By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return Service Details</returns>
        public async Task<ServiceBasicModel> GetByNameAsync(string name)
        {
            try
            {
                var result = await _repo.GetByNameAsync(name.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Service Existance By ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return True or False</returns>
        public async Task<bool?> IsExistByIDAsync(Guid serviceID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(serviceID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Service Existance By Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Return True or False</returns>
        public async Task<bool?> IsExistByNameAsync(string name)
        {
            try
            {
                var result = await _repo.IsExistByNameAsync(name.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Service Existance By Name, And exclude Current ID
        /// </summary>
        /// <param name="serviceID"></param>
        /// <param name="name"></param>
        /// <returns>Return True or False</returns>
        public async Task<bool?> IsExistByNameAsync(Guid serviceID, string name)
        {
            try
            {
                var result = await _repo.IsExistByNameAsync(serviceID, name.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Service 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Return Added Row GUID</returns>
        public async Task<Guid?> AddAsync(ServiceBasicModel entity)
        {
            try
            {
                entity.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(entity);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Service 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(ServiceBasicModel model)
        {
            try
            {
                model.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.UpdateAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }

        /// <summary>
        /// Delete Service 
        /// </summary>
        /// <param name="serviceID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid serviceID)
        {
            try
            {
                var result = await _repo.DeleteAsync(serviceID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return 0;
            }
        }
    }
}