using System;

namespace DentalCenterAPI.Models.Branch.Basic
{
    public class BranchBasicModel
    {
        public Guid BranchID { get; set; }
        public bool? IsMain { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public string FromDay { get; set; }
        public string ToDay { get; set; }
        public string FromHour { get; set; }
        public string ToHour { get; set; }
        public string PhoneNumber { get; set; }
        public string TextNumber { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}