using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class CostType
    {
        public CostType()
        {
            Departments = new HashSet<Department>();
        }

        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
