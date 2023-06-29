using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class StocktakeReport
    {
        public int Id { get; set; }
        public int StocktakeCycleId { get; set; }
        public string StockFullName { get; set; }
        public string PlantLocationName { get; set; }
        public string StoreName { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal ClosingQty { get; set; }
        public decimal Discrepancy { get; set; }
    }
}
