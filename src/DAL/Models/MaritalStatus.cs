using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
