using System.Linq;

namespace DAL
{
    public static class PriceLookUpLog
    {
        public static IQueryable<DAL.DTO.PriceLookupLog> getPriceLookUpLog()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PriceLookupLogs
               .Select(p => new DAL.DTO.PriceLookupLog
               {
                   Id = p.Id,
                   Stock = p.Stock,
                   OldPrice = p.OldPrice,
                   NewPrice = p.NewPrice,
                   Date = p.Date,
                   Comment = p.Comment,
                   Uom = p.Uom,
                   Application = p.Application,
                   Supplier = p.Supplier,
                   Username = p.Username,
                   InternalProductName = p.InternalProductName,
                   Currency = p.Currency
               });
            return source;
        }
    }
}
