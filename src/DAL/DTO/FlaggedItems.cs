using System;

namespace DAL.DTO
{
    public class FlaggedItems
    {
        public int Id { get; set; }
        public decimal Increase { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string IncreaseType { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string InternalProductName { get; set; }
        public string SupplierCode { get; set; }
        public string CompanyName { get; set; }
        public string IncreaseOrigin { get; set; }
    }
}
