using System.Linq;

namespace DAL
{
    public static class GroupingStock
    {
        public static IQueryable<DAL.DTO.GroupingStock> getGroupingStock()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
               .Select(p => new DAL.DTO.GroupingStock
               {
                   Id = p.Id,
                   Code = p.Stock.Code,
                   ProductName = p.Stock.ProductName,
                   InternalProductName = p.Stock.InternalProductName,
                   Uomid = p.Stock.Uomid,
                   UomName = p.Stock.Uom.Name,
                   StockCategoryId = p.Stock.StockCategoryId,
                   StockGroupId = p.Stock.StockGroupId,
                   MinThreshold = p.Stock.MinThreshold,
                   Price = p.Price,
                   Quantity = p.ItemQuantity,
                   DepartmentId = p.DepartmentId,
                   LocationId = p.LocationId,
                   StoreId = p.StoreId,
                   StoreTypeId = p.Store.StoreTypeId,
                   TotalPrice = (p.Price * p.ItemQuantity),
                   SupplierCurrencyId = p.Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Stock.Supplier.Currency.Iso,
                   StorageTypeId = p.Stock.StorageTypeId
               });
            return source;
        }
    }
}
