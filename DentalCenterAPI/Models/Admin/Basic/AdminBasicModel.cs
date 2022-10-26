using System;

namespace DentalCenterAPI.Models.Admin.Basic
{
    public class AdminBasicModel
    {
        public Guid AdminID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}