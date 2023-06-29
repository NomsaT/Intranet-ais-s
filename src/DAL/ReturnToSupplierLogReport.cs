using System.Linq;

namespace DAL
{
    public static class ReturnToSupplierLogReport
    {
        public static IQueryable<DAL.DTO.ReturnToSupplier> getReturnToSupplierLogReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ReturnToSuppliers
               .Where(i => i.LogEntry == true)
               .Select(p => new DAL.DTO.ReturnToSupplier
               {
                   Id = p.Id,
                   Code = p.Code,
                   ProductName = p.ProductName,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   ReturnedQuantity = p.ReturnedQuantity,
                   SupplierName = p.SupplierName,
                   DateReturned = p.DateReturned,
                   Price = p.Price,
                   LogEntry = p.LogEntry
               });
            return source;
        }
    }
}
