using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Appointment
    {
        public Guid Appointmentid { get; set; }
        public string Servicename { get; set; }
        public string Branchname { get; set; }
        public DateTime? Reservationdate { get; set; }
        public TimeSpan? Reservationtime { get; set; }
        public DateTime? Insertdate { get; set; }
        public Guid? Pateintid { get; set; }

        public virtual Patient Pateint { get; set; }
    }
}
