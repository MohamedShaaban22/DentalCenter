using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Blogdetail
    {
        public Guid Detailsid { get; set; }
        public string Title { get; set; }
        public string Discreption { get; set; }
        public string Imagepath { get; set; }
        public int? Detailsorder { get; set; }
        public Guid? Blogid { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual Blog Blog { get; set; }
    }
}
