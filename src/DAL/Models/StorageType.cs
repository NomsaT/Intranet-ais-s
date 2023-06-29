using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StorageType
    {
        public StorageType()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool RequireBarcode { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
