using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.HomeCounter.Basic;
using DentalCenterAPI.Repository.HomeCounter;

namespace DentalCenterAPI.Services.HomeCounter
{
    public class HomeCounterService : IHomeCounterService
    {
        private HomeCounterRepository _repo;
        public HomeCounterService(HomeCounterRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get Fisrt Record At Home Counter(For Admin)
        /// </summary>
        /// <returns>Return Home Counter Details</returns>
        public async Task<HomeCounterBasicModel> GetFirstAsync()
        {
            try
            {
                var result = await _repo.GetFirstAsync();
                return result;
            }
            catch (Exception ex)
            {
                Logger.Debug(ex);
                return null;
            }
        }

        /// <summary>
        /// Update Home Counter BY ID(For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        public async Task<int> UpdateAsync(HomeCounterBasicModel model)
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
                return -1;
            }
        }
    }
}