using System;

namespace DAL.DTO
{
    public class StocktakeLog
    {
        public int Id { get; set; }
        public string StockFullName { get; set; }
        public string PlantLocationName { get; set; }
        public string StoreName { get; set; }
        public decimal CurrentQty { get; set; }
        public decimal CountQty { get; set; }
        public bool Recount { get; set; }
        public DateTime StockTakeDate { get; set; }
        public DateTime? RecountDate { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Actions { get; set; }
        public string UserFullName { get; set; }
    }
}
