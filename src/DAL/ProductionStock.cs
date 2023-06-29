using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class ProductionStock
    {
        public static List<DAL.DTO.ProductionStock> getProductionStock()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities.Where(p => p.Store.Name == "Production")
                .AsEnumerable()
               .GroupBy(x => x.Stock.InternalProductName)
               .Select(p => new DAL.DTO.ProductionStock
               {
                   Id = p.First().Id,
                   Code = p.First().Stock.Code,
                   ProductName = p.First().Stock.ProductName,
                   InternalProductName = p.First().Stock.InternalProductName,
                   Uomid = p.First().Stock.Uomid,
                   UomName = p.First().Stock.Uom.Name,
                   StockCategoryId = p.First().Stock.StockCategoryId,
                   StockGroupId = p.First().Stock.StockGroupId,
                   MinThreshold = p.First().Stock.MinThreshold,
                   Price = p.First().Price,
                   Quantity = p.Sum(x => x.ItemQuantity),
                   DepartmentId = p.First().DepartmentId,
                   LocationId =p.First().LocationId,
                   StoreId = p.First().StoreId,
                   StoreTypeId = p.First().Store.StoreTypeId,
                   TotalPrice = p.Sum(x => x.Price),
                   SupplierCurrencyId = p.First().Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.First().Stock.Supplier.Currency.Iso,
                   StorageTypeId = p.First().Stock.StorageTypeId,
                   VerificationScan = p.First().VerificationScan
               }).ToList();
               
            return source;
        }
    }
}
