using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class RoleTemplate
    {
        public RoleTemplate()
        {
            RolePermissions = new HashSet<RolePermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
