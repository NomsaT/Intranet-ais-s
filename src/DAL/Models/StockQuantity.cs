using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StockQuantity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public decimal ItemQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Price { get; set; }
        public DateTime DateModified { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int StoreId { get; set; }
        public bool? BarcodePrinted { get; set; }
        public bool? VerificationScan { get; set; }
        public bool? Splitted { get; set; }
        public string GrnNumber { get; set; }

        public virtual Department Department { get; set; }
        public virtual PlantLocation Location { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Store Store { get; set; }
    }
}
