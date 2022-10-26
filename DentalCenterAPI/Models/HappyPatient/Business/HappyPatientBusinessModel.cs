using System.Collections.Generic;
using DentalCenterAPI.Models.HappyPatient.Basic;

namespace DentalCenterAPI.Models.HappyPatient.Business
{
    public class HappyPatientBusinessModel
    {
        public IEnumerable<HappyPatientBasicModel> FavoroitePatients { get; set; }
        public IEnumerable<HappyPatientBasicModel> AllPatients { get; set; }
    }
}