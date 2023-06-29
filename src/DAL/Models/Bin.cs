using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Bin
    {
        public Bin()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }

        public virtual Store Store { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
