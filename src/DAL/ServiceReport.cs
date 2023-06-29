using System.Linq;

namespace DAL
{
    public static class ServiceReport
    {
        public static IQueryable<DAL.DTO.Service> getServiceReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Services.Where(i => i.InternalOrder.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED)
               .Select(p => new DAL.DTO.Service
               {
                   Id = p.Id,
                   Description = p.Description,
                   InternalOrderId = p.InternalOrderId,
                   Value = p.Value,
                   VatAppl = p.VatAppl,
                   GrnAppl = p.GrnAppl,
                   GlcodeId = p.GlcodeId,
                   DateCreated = p.InternalOrder.DateApproved,
                   DepartmentId = p.DepartmentId,
                   SupplierCurrency = p.InternalOrder.Supplier.Currency.Iso
               });
            return source;
        }
    }
}
