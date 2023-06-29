using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class EmployeePosition
    {
        public EmployeePosition()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Position { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
