using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StocktakeReport
    {
        public int Id { get; set; }
        public int StocktakeCycleId { get; set; }
        public string StockFullName { get; set; }
        public string PlantLocationName { get; set; }
        public string StoreName { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal ClosingQty { get; set; }

        public virtual StocktakeCycle StocktakeCycle { get; set; }
    }
}
