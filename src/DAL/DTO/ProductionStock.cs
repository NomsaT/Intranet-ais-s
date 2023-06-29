
namespace DAL.DTO
{
    public class ProductionStock
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalProductName { get; set; }
        public int Uomid { get; set; }
        public string UomName { get; set; }
        public int StockCategoryId { get; set; }
        public decimal? MinThreshold { get; set; }
        public string OrderNr { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? StoreId { get; set; }
        public int? StoreTypeId { get; set; }
        public int? StockGroupId { get; set; }
        public int SupplierCurrencyId { get; set; }
        public string SupplierCurrencyIso { get; set; }
        public int? StorageTypeId { get; set; }
        public bool? VerificationScan { get; set; }
    }
}
