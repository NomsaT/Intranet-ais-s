using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PriceLookup
    {
        public PriceLookup()
        {
            PriceIncreases = new HashSet<PriceIncrease>();
        }

        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int SupplierId { get; set; }
        public int StockId { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<PriceIncrease> PriceIncreases { get; set; }
    }
}
