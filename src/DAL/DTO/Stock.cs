    namespace DAL.DTO
{
    public class Stock
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalSku { get; set; }
        public string InternalProductName { get; set; }
        public string InternalProductNamePack { get; set; }
        public string InternalProductNameFull { get; set; }
        public int Uomid { get; set; }
        public int StockCategoryId { get; set; }
        public decimal? MinThreshold { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuarantineQuantity { get; set; }
        public decimal ProductionQuantity { get; set; }
        public decimal InitialQuantity { get; set; }
        public decimal SecondQuantity { get; set; }
    
        public decimal Price { get; set; }
        public decimal PriceLookUpPrice { get; set; }
        public string UomName { get; set; }
        public string SecondUomName { get; set; }
        public string CategoryName { get; set; }
        public int? StockGroupId { get; set; }
        public bool Monitored { get; set; }
        public int StoreTypeId { get; set; }
        public int SupplierId { get; set; }
        public int SupplierCurrencyId { get; set; }
        public string SupplierCurrencyIso { get; set; }

        public decimal CurrentPrice { get; set; } //Price
        public int DefaultDepartmentId { get; set; }
        public int DefaultLocationId { get; set; }
        public int DefaultStoreId { get; set; }
        public int PackSize { get; set; } //amount
        public decimal PackQuantity { get; set; } //InitialQuantity
        public decimal ItemQuantity { get; set; } //InitialQuantity
        public decimal ItemPrice { get; set; }
        public int? SecondaryUomid { get; set; }
        public decimal? ComparisonSecondValue { get; set; }
        public decimal? CalculatedRatio { get; set; }
        public decimal? DeductedValue { get; set; }
        public decimal? ShippingDimensionsHeight { get; set; }
        public decimal? ProductWeight { get; set; }
        public decimal? ProductDimensionsLength { get; set; }
        public decimal? ProductDimensionsWidth { get; set; }
        public decimal? ProductDimensionsHeight { get; set; }
        public decimal? ShippingWeight { get; set; }
        public decimal? ShippingDimensionsLength { get; set; }
        public decimal? ShippingDimensionsWidth { get; set; }
        public int? StorageTypeId { get; set; }
        public int? ShelfLifeDays { get; set; }
        public int? ShelfLifeTypeId { get; set; }
        public string Barcode { get; set; }
        public string Username { get; set; }
        public int? BinId { get; set; }
    }
}
