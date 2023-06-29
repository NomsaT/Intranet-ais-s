using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductItemItems = new HashSet<ProductItem>();
            ProductItemProducts = new HashSet<ProductItem>();
            ProductStocks = new HashSet<ProductStock>();
            QuoteItems = new HashSet<QuoteItem>();
        }

        public int Id { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int DepartmentId { get; set; }
        public string Description { get; set; }
        public decimal? Quantity { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<ProductItem> ProductItemItems { get; set; }
        public virtual ICollection<ProductItem> ProductItemProducts { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
        public virtual ICollection<QuoteItem> QuoteItems { get; set; }
    }
}
