using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Happypatient
    {
        public Guid Happypatientid { get; set; }
        public string Videopath { get; set; }
        public bool? Isfavorite { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
