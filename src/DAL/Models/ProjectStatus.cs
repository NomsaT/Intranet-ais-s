using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ProjectStatus
    {
        public ProjectStatus()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
