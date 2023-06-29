using System.Linq;

namespace DAL
{
    public static class StockReport
    {
        public static IQueryable<DAL.DTO.StockLog> getStockReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockLogs
               .Select(p => new DAL.DTO.StockLog
               {
                   Id = p.Id,
                   ProductCodeName = p.ProductCodeName,
                   Date = p.Date,
                   Quantity = p.Quantity,
                   Price = p.Price,
                   Department = p.Department,
                   Action = p.Action,
                   Location = p.Location,
                   Store = p.Store,
                   StockCategory = p.StockCategory,
                   StockGroup = p.StockGroup,
                   Uom = p.Uom,
                   Supplier = p.Supplier,
                   InternalProductName = p.InternalProductName,
                   SupplierCurrency = p.SupplierCurrency
               });
            return source;
        }
    }
}
