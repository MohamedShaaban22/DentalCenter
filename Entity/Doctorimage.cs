using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Doctorimage
    {
        public Guid Doctorimageid { get; set; }
        public string Imagepath { get; set; }
        public Guid? Doctorid { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual Doctor Doctor { get; set; }
    }
}
