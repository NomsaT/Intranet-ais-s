using System;

namespace DAL.DTO
{
    public class PriceLookup
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int SupplierId { get; set; }
        public int StockId { get; set; }
        public int PriceIncreaseCount { get; set; }
        public int PriceIncreaseDateCount { get; set; }
        public decimal Discount { get; set; }
        public int SupplierCurrencyId { get; set; }
        public string Currency { get; set; }
        public string Username { get; set; }
    }
}
