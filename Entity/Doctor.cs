using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Doctor
    {
        public Guid Doctorid { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public string Referalnumber { get; set; }
        public string Imagepath { get; set; }
        public string Videopath { get; set; }
        public string Aboutdoctor { get; set; }
        public string Tiktokurl { get; set; }
        public string Facebookurl { get; set; }
        public string Instgramurl { get; set; }
        public string Linkedin { get; set; }
        public bool? Homedisplay { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
