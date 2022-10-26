using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Newsimage
    {
        public Guid Imageid { get; set; }
        public string Imagepath { get; set; }
        public Guid? Newsid { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual News News { get; set; }
    }
}
