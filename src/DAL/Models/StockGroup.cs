using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StockGroup
    {
        public StockGroup()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MinThreshold { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
