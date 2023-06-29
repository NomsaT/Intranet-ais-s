using System;

namespace DAL.DTO
{
    public class Stocktake
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

        //Recount-Notication info
        public string Stock { get; set; }
        public string Location { get; set; }
        public string Store { get; set; }
        public string UserFullName { get; set; }
    }
}
