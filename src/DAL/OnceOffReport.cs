using System.Linq;

namespace DAL
{
    public static class OnceOffReport
    {
        public static IQueryable<DAL.DTO.OnceOffItem> getOnceOffReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.OnceOffItems.Where(i => i.InternalOrder.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED)
               .Select(p => new DAL.DTO.OnceOffItem
               {
                   Id = p.Id,
                   Description = p.Description,
                   InternalOrderId = p.InternalOrderId,
                   Value = p.Value,
                   Quantity = p.Quantity,
                   Total = p.Total,
                   DepartmentId = p.DepartmentId,
                   Uomid = p.Uomid,
                   PackSize = p.PackSize,
                   VatAppl = p.VatAppl,
                   GrnAppl = p.GrnAppl,
                   GlcodeId = p.GlcodeId,
                   DateCreated = p.InternalOrder.DateApproved,
                   SupplierCurrency = p.InternalOrder.Supplier.Currency.Iso
               });
            return source;
        }
    }
}
