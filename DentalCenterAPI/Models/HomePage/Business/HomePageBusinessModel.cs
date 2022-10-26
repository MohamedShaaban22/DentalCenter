using System.Collections.Generic;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.Gallery.Basic;
using DentalCenterAPI.Models.HappyPatient.Basic;
using DentalCenterAPI.Models.HomeCounter.Basic;

namespace DentalCenterAPI.Models.HomePage.Business
{
    public class HomePageBusinessModel
    {
        public HomeCounterBasicModel Counters { get; set; }
        public IEnumerable<GalleryBasicModel> Gallery { get; set; }
        public IEnumerable<DoctorBasicModel> Doctors { get; set; }
        public IEnumerable<HappyPatientBasicModel> HappyPatientsReviews { get; set; }
    }
}