using System.Threading.Tasks;
using DentalCenterAPI.Models.HomePage.Business;

namespace DentalCenterAPI.Services.HomePage
{
    public interface IHomePageService
    {
        /// <summary>
        /// Get Home Page (Counters, Favorite Gallery, Home Doctors, Favorite HappyPatients Revirews) (For Web)
        /// </summary>
        /// <returns>Return Home Page Details</returns>
        Task<HomePageBusinessModel> GetAllAsync();
    }
}