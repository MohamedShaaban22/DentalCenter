using System;

namespace DentalCenterAPI.Models.HomeCounter.Basic
{
    public class HomeCounterBasicModel
    {
        public Guid HomeCounterID { get; set; }
        public int? DoctorsCount { get; set; }
        public int? HappypatientsCount { get; set; }
        public int? BranchesCount { get; set; }
        public int? ExpYearsCount { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}