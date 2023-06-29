using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StoreType
    {
        public StoreType()
        {
            Stores = new HashSet<Store>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}
