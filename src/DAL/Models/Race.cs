using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Race
    {
        public Race()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Race1 { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
