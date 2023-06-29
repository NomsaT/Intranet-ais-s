using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Gender
    {
        public Gender()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Gender1 { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
