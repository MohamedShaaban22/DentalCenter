using System;
using System.Threading.Tasks;
using DentalCenterAPI.Configurations.Logging;
using DentalCenterAPI.Models.HomePage.Business;
using DentalCenterAPI.Repository.HomePage;

namespace DentalCenterAPI.Services.HomePage
{
    public class HomePageService : IHomePageService
    {
        private HomePageRepository _repo;
        public HomePageService(HomePageRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get Home Page (Counters, Favorite Gallery, Home Doctors, Favorite HappyPatients Revirews) (For Web)
        /// </summary>
        /// <returns>Return Home Page Details</returns>
        public async Task<HomePageBusinessModel> GetAllAsync()
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
    }
}