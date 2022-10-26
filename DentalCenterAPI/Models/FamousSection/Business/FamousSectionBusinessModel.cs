using System.Collections.Generic;
using DentalCenterAPI.Models.FamousSection.Basic;

namespace DentalCenterAPI.Models.FamousSection.Business
{
    public class FamousSectionBusinessModel : FamousSectionBasicModel
    {
        public IEnumerable<FamousSectionImageBusinessModel> FamousSectionImages { get; set; }
    }
}