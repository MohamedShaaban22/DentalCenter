using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Serviceimage
    {
        public Guid Serviceimagesid { get; set; }
        public string Servicename { get; set; }
        public string Beforeimagepath { get; set; }
        public string Afterimagepath { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
