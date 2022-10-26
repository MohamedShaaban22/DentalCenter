using System;
using System.Collections.Generic;

#nullable disable

namespace PR_DentalCenterAPI.Entity
{
    public partial class Blog
    {
        public Blog()
        {
            Blogdetails = new HashSet<Blogdetail>();
        }

        public Guid Blogid { get; set; }
        public string Title { get; set; }
        public string Writter { get; set; }
        public DateTime? Insertdate { get; set; }

        public virtual ICollection<Blogdetail> Blogdetails { get; set; }
    }
}
