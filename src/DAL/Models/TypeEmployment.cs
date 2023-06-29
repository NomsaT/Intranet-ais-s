using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class TypeEmployment
    {
        public TypeEmployment()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
