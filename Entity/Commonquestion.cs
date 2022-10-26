using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Commonquestion
    {
        public Guid Commonquestionid { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
