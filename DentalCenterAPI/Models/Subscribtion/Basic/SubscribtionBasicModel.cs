using System;

namespace DentalCenterAPI.Models.Subscribtion.Basic
{
    public class SubscribtionBasicModel
    {
        public Guid SubscribtionID { get; set; }
        public string Email { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}