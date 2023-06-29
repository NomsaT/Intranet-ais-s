
using System;

namespace DAL.DTO
{
    public class ReturnToSupplier
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalSku { get; set; }
        public string InternalProductName { get; set; }
        public decimal ReturnedQuantity { get; set; }
        public string SupplierName { get; set; }
        public DateTime DateReturned { get; set; }
        public decimal? Price { get; set; }
        public bool? LogEntry { get; set; }
    }
}