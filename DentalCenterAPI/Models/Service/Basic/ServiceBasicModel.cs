using System;

namespace DentalCenterAPI.Models.Service.Basic
{
    public class ServiceBasicModel
    {
        public Guid ServiceID { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string VideoPath { get; set; }
        public string InfoPartOne { get; set; }
        public string InfoPartTwo { get; set; }
        public DateTime? InsertDate { get; set; }
    }
}