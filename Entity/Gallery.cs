using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Gallery
    {
        public Guid Galleryid { get; set; }
        public string Type { get; set; }
        public string Imagepath { get; set; }
        public DateTime? Insertdate { get; set; }
    }
}
