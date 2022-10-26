using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Testmonial
    {
        public Guid Testmonialid { get; set; }
        public string Videopath { get; set; }
        public string Imagepath { get; set; }
        public string Quote { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
