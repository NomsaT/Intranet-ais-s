using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Stocktake
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int PlantLocationId { get; set; }
        public int StoreId { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal CapturedQty { get; set; }
        public bool? Recount { get; set; }
        public DateTime? StockTakeDate { get; set; }
        public int? UserId { get; set; }

        public virtual PlantLocation PlantLocation { get; set; }
        public virtual Stock Stock { get; set; }
        public virtual Store Store { get; set; }
        public virtual User User { get; set; }
    }
}
