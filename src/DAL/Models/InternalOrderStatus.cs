using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InternalOrderStatus
    {
        public InternalOrderStatus()
        {
            InternalOrders = new HashSet<InternalOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InternalOrder> InternalOrders { get; set; }
    }
}
