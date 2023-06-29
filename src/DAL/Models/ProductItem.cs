using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ProductItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ItemId { get; set; }
        public decimal Quantity { get; set; }

        public virtual Product Item { get; set; }
        public virtual Product Product { get; set; }
    }
}
