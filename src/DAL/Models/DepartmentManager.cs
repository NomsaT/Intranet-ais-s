using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class DepartmentManager
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
        public virtual User User { get; set; }
    }
}
