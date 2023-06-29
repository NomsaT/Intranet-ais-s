using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class Chart
    {

        public static object getOrders()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " - " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " - " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DateApproved = p.DateApproved,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       //Stock = c.Stock.Code + " - " + c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       StockName = c.Stock.ProductName + " - " + c.Stock.InternalProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       Uomid = c.Uomid,
                       UomName = c.Stock.Uom.Name,
                       PackSize = c.PackSize
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Description = f.Description,
                       InternalOrderId = f.InternalOrderId,
                       Value = f.Value,
                       Quantity = f.Quantity,
                       DepartmentId = f.DepartmentId,
                       DepartmentName = f.Department.Name,
                       Uomid = f.Uomid,
                       UomName = f.Uom.Name,
                       PackSize = f.PackSize,
                       VatAppl = f.VatAppl,
                       GrnAppl = f.GrnAppl,
                       GlcodeId = f.GlcodeId,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED);
            return source;
        }
        public static object getDepartmentStock()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext(); //accessing context class
            var source = (from grn in db.Grns.AsEnumerable()
                          join IO in db.InternalOrders on grn.InternalOrderId equals IO.Id
                          join IOT in db.InternalOrderItems on IO.Id equals IOT.InternalOrderId
                          join Dep in db.Departments on IOT.DepartmentId equals Dep.Id
                          join OOI in db.OnceOffItems on IO.Id equals OOI.InternalOrderId
                          select new
                          {
                              ID = grn.Id,
                              GrnNumber = grn.GrnNumber,
                              InternalOrderID = IO.Id,
                              DepartmentName = Dep.Name + "(" +Dep.CostType.Abbreviation +")",
                              Total = IO.InternalOrderItems.Sum(x => x.Total) + OOI.Total,
                          }).Distinct().ToList();

            return source;
        }


    }
}
