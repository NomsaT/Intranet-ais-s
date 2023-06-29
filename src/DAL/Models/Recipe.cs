using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Recipe
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int StockComponentId { get; set; }
        public decimal Quantity { get; set; }

        public virtual Stock Stock { get; set; }
        public virtual Stock StockComponent { get; set; }
    }
}
