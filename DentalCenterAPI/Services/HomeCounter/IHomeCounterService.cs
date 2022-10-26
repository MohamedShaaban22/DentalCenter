using System.Threading.Tasks;
using DentalCenterAPI.Models.HomeCounter.Basic;

namespace DentalCenterAPI.Services.HomeCounter
{
    public interface IHomeCounterService
    {
        /// <summary>
        /// Get Fisrt Record At Home Counter(For Admin)
        /// </summary>
        /// <returns>Return Home Counter Details</returns>
        Task<HomeCounterBasicModel> GetFirstAsync();

        /// <summary>
        /// Update Home Counter BY ID(For Admin)
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Return Updated Rows Count</returns>
        Task<int> UpdateAsync(HomeCounterBasicModel model);
    }
}