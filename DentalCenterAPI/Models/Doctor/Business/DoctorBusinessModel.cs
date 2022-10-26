using DentalCenterAPI.Models.Blog.Basic;
using DentalCenterAPI.Models.Doctor.Basic;
using DentalCenterAPI.Models.DoctorPatient.Basic;
using System.Collections.Generic;

namespace DentalCenterAPI.Models.Doctor.Business
{
    public class DoctorBusinessModel : DoctorBasicModel
    {
        public IEnumerable<DoctorPatientBasicModel> DoctorPatients { get; set; }
        public IEnumerable<BlogBasicModel> DoctorBlogs { get; set; }
    }
}