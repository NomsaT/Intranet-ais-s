using System;

namespace DAL.DTO
{
    public class PriceIncrease
    {
        public int Id { get; set; }
        public decimal Increase { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int IncreaseTypeId { get; set; }
        public string IncreaseTypeName { get; set; }
        public int PriceLookUpId { get; set; }
        public string IncreaseOrigin { get; set; }
        public string Username { get; set; }
    }
}
