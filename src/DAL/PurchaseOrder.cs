using DAL.DTO;
using System;
using System.Globalization;
using System.Linq;

namespace DAL
{
    public static class PurchaseOrder
    {
        public static DAL.DTO.PurchaseOrder getPurchaseOrder(int PurchaseOrderId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
                .Where(p => p.Id == PurchaseOrderId)
               .Select(p => new DAL.DTO.PurchaseOrder
               {
                   Id = p.Id,
                   RequestByFullName = p.RequestedBy.Name + " " + p.RequestedBy.Surname,
                   PONumber = p.Id,
                   QuotationNumber = p.QuotationNumber,
                   CompanyInternalName = p.Company.Name,
                   DateCreated = System.DateTime.Now,
                   DeliveryDate = (DateTime)p.DeliveryDate,
                   Total = (decimal)p.Total,
                   VAT = (decimal)p.Vat,
                   CompanyName = p.Supplier.CompanyName,
                   SupplierPhysicalStreetAddress1 = p.Supplier.Address.StreetAddress1,
                   SupplierSuburb = p.Supplier.Address.Suburb,
                   SupplierPhysicalCity = p.Supplier.Address.City,
                   SupplierPostCode = p.Supplier.Address.PostCode,
                   PlantLocationStreetAddress1 = p.PlantLocation.Address.StreetAddress1,
                   PlantLocationPhysicalCity = p.PlantLocation.Address.City,
                   PlantLocationSuburb = p.PlantLocation.Address.Suburb,
                   PlantLocationPostalCode = p.PlantLocation.Address.PostCode,
                   InternalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       StockName = c.Stock.Code + " - " + c.Stock.ProductName,
                       Code = c.Stock.Code,
                       ProductName = c.Stock.ProductName,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       Total = c.Total,
                       DepartmentId = c.DepartmentId,
                       DepartmentName = c.Department.Name,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl
                   }).ToList(),
                   onceOffItems = p.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
                   {
                       Id = f.Id,
                       Code = f.Code,
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
                   }).ToList(),
                   services = p.Services.Select(s => new DAL.DTO.Service
                   {
                       Id = s.Id,
                       Description = s.Description,
                       Code = s.Code,
                       Quantity = s.Quantity,
                       InternalOrderId = s.InternalOrderId,
                       Value = s.Value,
                       VatAppl = s.VatAppl,
                       GrnAppl = s.GrnAppl,
                       GlcodeId = s.GlcodeId
                   }).ToList()
               }).FirstOrDefault();
            return source;
        }

        public static string getCurrentVat()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Vats
               .Select(p => new DAL.DTO.Vat
               {
                   Vat1 = p.Vat1
               }).FirstOrDefault();

       
            return source.Vat1.ToString();
                
        }
    }
}
