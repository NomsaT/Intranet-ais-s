using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Permission
    {
        public Permission()
        {
            RolePermissions = new HashSet<RolePermission>();
            UserPermissions = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Page { get; set; }
        public string Component { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
