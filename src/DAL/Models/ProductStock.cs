using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ProductStock
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyPerSquareMeter { get; set; }
        public decimal SquaresUsed { get; set; }

        public virtual Product Product { get; set; }
        public virtual Stock Stock { get; set; }
    }
}
