using System;

namespace DAL.DTO
{
    public class StockQuantity
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string Code { get; set; }
        public string StockFullName { get; set; }
        public int PackSize { get; set; }
        public decimal ItemQuantity { get; set; }
        public DateTime DateCreated { get; set; }
        public decimal Price { get; set; }
        public DateTime DateModified { get; set; }
        public int DepartmentId { get; set; }
        public int LocationId { get; set; }
        public int StoreId { get; set; }
        public int uomid { get; set; }
        public string UomName { get; set; }
        public int SupplierCurrencyId { get; set; }
        public string SupplierCurrencyIso { get; set; }
        public string Barcode { get; set; }
        public bool? BarcodePrinted { get; set; }
        public bool? VerificationScan { get; set; }
        public bool? Splitted { get; set; }
        public string GrnNumber { get; set; }
    }
}
