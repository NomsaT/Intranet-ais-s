using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PriceLookupLog
    {
        public int Id { get; set; }
        public string Stock { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string Uom { get; set; }
        public string Application { get; set; }
        public string Supplier { get; set; }
        public string Username { get; set; }
        public string InternalProductName { get; set; }
        public string Currency { get; set; }
    }
}
