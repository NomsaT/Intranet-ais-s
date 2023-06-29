using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StockCategory
    {
        public StockCategory()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
