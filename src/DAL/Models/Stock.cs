using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Stock
    {
        public Stock()
        {
            InternalOrderItems = new HashSet<InternalOrderItem>();
            PriceLookups = new HashSet<PriceLookup>();
            ProductStocks = new HashSet<ProductStock>();
            RecipeStockComponents = new HashSet<Recipe>();
            RecipeStocks = new HashSet<Recipe>();
            StockQuantities = new HashSet<StockQuantity>();
            Stocktakes = new HashSet<Stocktake>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalSku { get; set; }
        public string InternalProductName { get; set; }
        public int Uomid { get; set; }
        public int StockCategoryId { get; set; }
        public decimal? MinThreshold { get; set; }
        public int? StockGroupId { get; set; }
        public bool Monitored { get; set; }
        public int SupplierId { get; set; }
        public decimal CurrentPrice { get; set; }
        public int DefaultDepartmentId { get; set; }
        public int DefaultLocationId { get; set; }
        public int DefaultStoreId { get; set; }
        public int PackSize { get; set; }
        public decimal PackQuantity { get; set; }
        public decimal ItemQuantity { get; set; }
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
        public int? BinId { get; set; }

        public virtual Bin Bin { get; set; }
        public virtual Department DefaultDepartment { get; set; }
        public virtual PlantLocation DefaultLocation { get; set; }
        public virtual Store DefaultStore { get; set; }
        public virtual UnitOfMeasurement SecondaryUom { get; set; }
        public virtual ShelfLifeType ShelfLifeType { get; set; }
        public virtual StockCategory StockCategory { get; set; }
        public virtual StockGroup StockGroup { get; set; }
        public virtual StorageType StorageType { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual UnitOfMeasurement Uom { get; set; }
        public virtual ICollection<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual ICollection<PriceLookup> PriceLookups { get; set; }
        public virtual ICollection<ProductStock> ProductStocks { get; set; }
        public virtual ICollection<Recipe> RecipeStockComponents { get; set; }
        public virtual ICollection<Recipe> RecipeStocks { get; set; }
        public virtual ICollection<StockQuantity> StockQuantities { get; set; }
        public virtual ICollection<Stocktake> Stocktakes { get; set; }
    }
}
