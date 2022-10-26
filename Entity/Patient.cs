using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public Guid Patientid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string Contactmethod { get; set; }
        public string Message { get; set; }
        public bool? Isnewuser { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
