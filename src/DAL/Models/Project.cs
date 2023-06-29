using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Project
    {
        public Project()
        {
            InternalOrders = new HashSet<InternalOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectStatusId { get; set; }
        public decimal Budget { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }
        public virtual ICollection<InternalOrder> InternalOrders { get; set; }
    }
}
