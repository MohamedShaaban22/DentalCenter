using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Homecounter
    {
        public Guid Homecounterid { get; set; }
        public int? Doctorscount { get; set; }
        public int? Happypatientscount { get; set; }
        public int? Branchescount { get; set; }
        public int? Expyearscount { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
