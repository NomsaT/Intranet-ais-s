using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class DepartmentStock
    {
        public static IQueryable<DAL.DTO.DepartmentStock> getDepartmentStock()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
               .Where(i => i.Store.ProductionStore == false)
               .Select(p => new DAL.DTO.DepartmentStock
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
                   StorageTypeId = p.Stock.StorageTypeId,
                   VerificationScan = p.VerificationScan
               });
            return source;
        }

        public static async Task<int> editDepartmentStock(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StockQuantities.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentUsersException("Item does not exist.");

            JsonConvert.PopulateObject(values, Obj);

            await db.SaveChangesAsync();

            return Obj.Id;
        }
    }
}
