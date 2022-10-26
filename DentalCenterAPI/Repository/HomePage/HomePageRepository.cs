using System.Data;
using System.Threading.Tasks;
using Dapper;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.Gallery.Basic;
using DentalCenterAPI.Models.HappyPatient.Basic;
using DentalCenterAPI.Models.HomeCounter.Basic;
using DentalCenterAPI.Models.HomePage.Business;
using Microsoft.Data.SqlClient;

namespace DentalCenterAPI.Repository.HomePage
{
    public class HomePageRepository
    {
        private IDbConnection _db;
        private string connectionString = Utility.Utility.GetDatabaseConnectionstring();
        public HomePageRepository()
        {
            _db = new SqlConnection(connectionString);
        }

         public async Task<HomePageBusinessModel> GetAllAsync()
        {
            HomePageBusinessModel result = new HomePageBusinessModel();
            string query = @"SELECT [galleryid],[type],[imagepath] from [gallery] Where [isfavorite] = 1 
                            SELECT Top(1) [doctorscount],[happypatientscount],[branchescount],[expyearscount] from [homecounter]
                            SELECT [doctorid],[name],[firstdiscription],[referalnumber],[homeimagepath],[tiktokurl],[facebookurl],[instgramurl],[linkedinurl] from [doctor]
                                     Where [homedisplay] = 1 
                            SELECT [happypatientid],[imagepath],[name],[job],[comment] from [happypatient] Where [isfavorite] = 1 and LOWER(type) = 'review' ";
            using (var multi = await _db.QueryMultipleAsync(query))
            {
                 result.Gallery = await multi.ReadAsync<GalleryBasicModel>();
                 result.Counters = await multi.ReadFirstOrDefaultAsync<HomeCounterBasicModel>();
                 result.Doctors = await multi.ReadAsync<DoctorBasicModel>();
                 result.HappyPatientsReviews = await multi.ReadAsync<HappyPatientBasicModel>();
                return result;
            }
        }
    }
}