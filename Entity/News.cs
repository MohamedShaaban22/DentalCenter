using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class News
    {
        public News()
        {
            Newsimages = new HashSet<Newsimage>();
        }

        public Guid Newsid { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Imagepath { get; set; }
        public string Discription { get; set; }
        public string Writter { get; set; }
        public string Firstdetails { get; set; }
        public string Seconddetails { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual ICollection<Newsimage> Newsimages { get; set; }
    }
}
