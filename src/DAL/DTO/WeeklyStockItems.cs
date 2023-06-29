using System;

namespace DAL.DTO
{
    public class WeeklyStockItems
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalProductName { get; set; }
        public DateTime Date { get; set; }
        public string UomType { get; set; }
        public decimal Quantity { get; set; }
        public string StockCategory { get; set; }
    }
}
