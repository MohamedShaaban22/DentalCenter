using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Subscribtion
    {
        public Guid Subscribtionid { get; set; }
        public string Email { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
