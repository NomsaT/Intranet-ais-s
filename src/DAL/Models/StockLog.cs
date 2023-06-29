using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StockLog
    {
        public int Id { get; set; }
        public string ProductCodeName { get; set; }
        public DateTime Date { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Department { get; set; }
        public string Action { get; set; }
        public string Location { get; set; }
        public string Store { get; set; }
        public string StockCategory { get; set; }
        public string StockGroup { get; set; }
        public string Uom { get; set; }
        public string Supplier { get; set; }
        public string InternalProductName { get; set; }
        public string SupplierCurrency { get; set; }
    }
}
