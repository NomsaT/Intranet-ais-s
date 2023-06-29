using System.Linq;

namespace DAL
{
    public static class OrdersReport
    {
        public static IQueryable<DAL.DTO.InternalOrder> getOrdersReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   DateApproved = p.DateApproved,
                   InternalComment = p.InternalComment,
                   PlantLocationId = p.PlantLocationId,
                   SupplierComment = p.SupplierComment,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       Uomid = f.Uomid,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       Code = f.Code
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE || a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED);
            return source;
        }
    }
}
