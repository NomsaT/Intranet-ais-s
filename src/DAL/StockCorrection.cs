using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StockCorrection
    {
        public static IQueryable<DAL.DTO.StockQuantity> getStockCorrection()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
               .Select(p => new DAL.DTO.StockQuantity
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   ItemQuantity = p.ItemQuantity,
                   DateCreated = p.DateCreated,
                   DateModified = p.DateModified,
                   Price = p.Price,
                   DepartmentId = p.DepartmentId,
                   LocationId = p.LocationId,
                   StoreId = p.StoreId,
                   uomid = p.Stock.Uomid,
                   UomName = p.Stock.Uom.Name,
                   SupplierCurrencyId = p.Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Stock.Supplier.Currency.Iso,
                   Barcode = "STQ" + p.Id.ToString("D9")
               });
            return source;
        }

        public static async Task<int> editStockCorrection(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StockQuantities.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockCorrectionException("Stock does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            Obj.DateModified = DateTime.Now;
            await db.SaveChangesAsync();

            return Obj.Id;
        }

    }
}
