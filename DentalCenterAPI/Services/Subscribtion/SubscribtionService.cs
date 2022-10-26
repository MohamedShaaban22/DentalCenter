using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.Subscribtion.Basic;
using DentalCenterAPI.Repository.Subscribtion;

namespace DentalCenterAPI.Services.Subscribtion
{
    public class SubscribtionService : ISubscribtionService
    {
        private SubscribtionRepository _repo;
        private IMapper _mapper;
        public SubscribtionService(SubscribtionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        /// <summary>
        /// Get List of Subscribtions (For Admin)
        /// </summary>
        /// <returns>Return List of Subscribtions</returns>
        public async Task<IEnumerable<SubscribtionBasicModel>> GetAllAsync()
        {
            try
            {
                var result = await _repo.GetAllAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Subscribtion Existance (For Admin)
        /// </summary>
        /// <param name="subscribtionID"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsExistAsync(Guid subscribtionID)
        {
            try
            {
                var result = await _repo.IsExistByIDAsync(subscribtionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Check Subscribtion email Existance (For Admin)
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Return (True, False)</returns>
        public async Task<bool?> IsEmailExistAsync(string email)
        {
            try
            {
                var result = await _repo.IsEmailExistAsync(email.ToLower());
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Add Subscribtion  (For Admin, Web)
        /// </summary>
        /// <param name="subscribtion"></param>
        /// <returns>Return Added Subscribtion GUID</returns>
        public async Task<Guid?> AddAsync(SubscribtionBasicModel subscribtion)
        {
            try
            {
                subscribtion.Email = subscribtion.Email.ToLower();
                subscribtion.InsertDate = Utility.Utility.GetDateTimeByTimeZone();
                var result = await _repo.AddAsync(subscribtion);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Delete Subscribtion (For Admin)
        /// </summary>
        /// <param name="subscribtionID"></param>
        /// <returns>Return Deleted Rows Count</returns>
        public async Task<int> DeleteAsync(Guid subscribtionID)
        {
            try
            {
                var result = await _repo.DeleteAsync(subscribtionID);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return -1;
            }
        }
    }
}