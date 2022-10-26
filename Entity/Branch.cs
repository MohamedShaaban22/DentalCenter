using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Branch
    {
        public Guid Branchid { get; set; }
        public bool? Ismain { get; set; }
        public string Name { get; set; }
        public string Imagepath { get; set; }
        public string Address { get; set; }
        public string Fromday { get; set; }
        public string Today { get; set; }
        public string Fromhour { get; set; }
        public string Tohour { get; set; }
        public string Phonenumber { get; set; }
        public string Textnumber { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
