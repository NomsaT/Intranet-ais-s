using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class InternalOrder
    {
        public static IQueryable<DAL.DTO.InternalOrder> getInternalOrder()
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
                   ApproveByFullName= p.ApproveBy.Name + " " + p.ApproveBy.Surname,
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
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       UomName = c.Uom.Name,
                       PackSize = c.PackSize,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       DateCreated = p.DateCreated,
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
                       DateCreated = p.DateCreated,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED || a.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING || a.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL || a.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW || (a.DateApproved.HasValue && a.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE) || a.StatusId == (int)DAL.Constants.InternalOrderStatus.DRAFT)
               .OrderByDescending(i => i.Id);

            return source;
        }

        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrders()
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
                   ApproveByFullName = p.ApproveBy.Name + " " + p.ApproveBy.Surname,
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
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       UomName = c.Uom.Name,
                       PackSize = c.PackSize,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       DateCreated = p.DateCreated,
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
                       DateCreated = p.DateCreated,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED)
               .OrderByDescending(i => i.Id);

            return source;
        }


        public static IQueryable<DAL.DTO.InternalOrder> getInternalOdersByDateCreated(DateTime startDate, DateTime endDate)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int endDataExist = endDate.CompareTo(new DateTime());//check if we have a valid end date
            int startDateExist = startDate.CompareTo(new DateTime());//check if we have a valid start date

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
                   ApproveByFullName = p.ApproveBy.Name + " " + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated.Value.Date,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   DateApproved = p.DateApproved,
                   InternalComment = p.InternalComment,
                   PlantLocationId = p.PlantLocationId,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       UomName = c.Uom.Name,
                       PackSize = c.PackSize,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       Total = f.Quantity * f.Value,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                       DateCreated = p.DateCreated,
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
                       DateCreated = p.DateCreated,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               }).Where(a => (a.DateCreated.Value.Date >= startDate && endDataExist==0) || 
                  (startDateExist == 0 &&  a.DateCreated.Value.Date <= endDate.Date)
                  || (startDateExist !=0 && endDataExist != 0 &&
                  a.DateCreated.Value.Date >= startDate.Date && a.DateCreated.Value.Date <= endDate.Date))
               .OrderByDescending(i => i.Id);

            return source;
        }

        public static object getAllOrders()
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
                   ApproveByFullName = p.ApproveBy.Name + " -" + p.ApproveBy.Surname,
                   Comment = p.Comment,
                   FollowUpDate = p.FollowUpDate,
                   DateCreated = p.DateCreated,
                   DeliveryDate = p.DeliveryDate,
                   Total = p.Total,
                   StatusId = p.StatusId,
                   StatusDisplay = p.Status.Name,
                   PlantLocationId = p.PlantLocationId,
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
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
                       PackSize = c.PackSize,
                       UomPrice = c.UomPrice
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
               }).Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE|| a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED  || a.StatusId == (int)DAL.Constants.InternalOrderStatus.REVIEW || a.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDING);
            return source;
        }
        public static DAL.DTO.InternalOrder getInternalOrderDataEdit(int internalOrderId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.InternalOrder data = new DAL.DTO.InternalOrder();

            var IO = db.InternalOrders.Find(internalOrderId);


            data = new DAL.DTO.InternalOrder
            {
                Id = IO.Id,
                RequestedById = IO.RequestedById,
                RequestByFullName = IO.RequestedBy.Name + " " + IO.RequestedBy.Surname,
                PlacedById = IO.PlacedById,
                PlacedByFullName = IO.PlacedBy.Name + " " + IO.PlacedBy.Surname,
                QuotationNumber = IO.QuotationNumber,
                SupplierId = IO.SupplierId,
                SupplierFullName = IO.SupplierId != null ? (IO.Supplier.CompanyName) : null,                
                ApproveById = IO.ApproveById,
                ApproveByFullName = IO.ApproveById != null ? (IO.ApproveBy.Name + " " + IO.ApproveBy.Surname) : null,
                Comment = IO.Comment,
                FollowUpDate = IO.FollowUpDate,
                DateCreated = IO.DateCreated,
                DeliveryDate = IO.DeliveryDate,
                Total = IO.Total,
                StatusId = IO.StatusId,
                StatusDisplay = IO.Status.Name,
                DateApproved = IO.DateApproved,
                InternalComment = IO.InternalComment,
                PlantLocationId = IO.PlantLocationId,
                PlantLocationName = IO.PlantLocationId != null ? (IO.PlantLocation.Name) : null,
                SupplierComment = IO.SupplierComment,
                SupplierCurrency = IO.SupplierId != null ? (IO.Supplier.Currency.Iso) : null,
                Vat = IO.Vat,
                ProjectId = IO.ProjectId,
                ProjectName = IO.ProjectId != null ? (IO.Project.Name) : null,
                CompanyId = IO.CompanyId,
                CompanyName = IO.CompanyId != null ? (IO.Company.Name) : null
            };

            data.internalOrderItems = IO.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
            {
                Id = c.Id,
                InternalOrderId = c.InternalOrderId,
                StockId = c.StockId,
                StockName = c.Stock.InternalProductName,
                OriginalValue = c.OriginalValue,
                Value = c.Value,
                Quantity = c.Quantity,
                GlcodeId = c.GlcodeId,
                GlFullName = c.Glcode != null ? (c.Glcode.Code + " - " + c.Glcode.Name) : null,
                DepartmentId = c.DepartmentId,
                DepartmentName = c.Department.Name,
                Uomid = c.Uomid,
                UomName = c.Uom.Name,
                PackSize = c.PackSize,
                Total = c.Total,
                VatAppl = c.VatAppl,
                GrnAppl = c.GrnAppl,
                TotalUnits = c.Quantity * c.PackSize,
                UomPrice = c.UomPrice
            }).ToList();
            data.onceOffItems = IO.OnceOffItems.Select(f => new DAL.DTO.OnceOffItem
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
                GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                DateCreated = IO.DateApproved,
                TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
                Total = f.Total,
                Code = f.Code
            }).ToList();
            data.services = IO.Services.Select(s => new DAL.DTO.Service
            {
                Id = s.Id,
                Description = s.Description,
                InternalOrderId = s.InternalOrderId,
                Value = s.Value,
                VatAppl = s.VatAppl,
                GrnAppl = s.GrnAppl,
                GlcodeId = s.GlcodeId,
                GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                DateCreated = IO.DateApproved,
                DepartmentId = s.DepartmentId,
                DepartmentName = s.Department.Name,
                Quantity = s.Quantity,
                Code = s.Code,
                Total = s.Quantity * s.Value
            }).ToList();

            return data;
        }

        public static int addInternalOrder(DAL.DTO.InternalOrder values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.InternalOrder();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj);

            //TODO: do validation in the front-end
            if (Obj.InternalOrderItems.GroupBy(x => x.StockId).Any(g => g.Count() > 1))
            {
                throw new InternalOrdersException("Duplicate stock line item, update the quantity instead.");
            }            

            Obj.DateCreated = DateTime.Now;
            Obj.StatusId = (int)DAL.Constants.InternalOrderStatus.PENDING;
            db.InternalOrders.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static int addInternalOrderDraft(DAL.DTO.InternalOrder values)
        {
            //save a draft of the internal order
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.InternalOrder();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj);

            if (Obj.InternalOrderItems.GroupBy(x => x.StockId).Any(g => g.Count() > 1))
            {
                throw new InternalOrdersException("Duplicate stock line item, update the quantity instead.");
            }

            if(Obj.DeliveryDate.ToString() == "0001/01/01 00:00:00")
            {
                Obj.DeliveryDate = DateTime.Now;
            }

            if (Obj.FollowUpDate.ToString() == "0001/01/01 00:00:00")
            {
                Obj.FollowUpDate = DateTime.Now;
            }

            Obj.DateCreated = DateTime.Now;
            Obj.StatusId = (int)DAL.Constants.InternalOrderStatus.DRAFT;
            db.InternalOrders.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static int editInternalOrder(int key, DAL.DTO.InternalOrder values)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.InternalOrders.FirstOrDefault(o => o.Id == key);
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            DTO.InternalOrder changes = new DTO.InternalOrder();
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), changes, serializerSettings);

            if (changes.internalOrderItems != null)
            {
                db.InternalOrderItems.RemoveRange(Obj.InternalOrderItems);
                Obj.InternalOrderItems.Clear();
            }

            if (changes.onceOffItems != null)
            {
                db.OnceOffItems.RemoveRange(Obj.OnceOffItems);
                Obj.OnceOffItems.Clear();
            }
            if (changes.services != null)
            {
                db.Services.RemoveRange(Obj.Services);
                Obj.Services.Clear();
            }

            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);

            if (Obj.InternalOrderItems.GroupBy(x => x.StockId).Any(g => g.Count() > 1))
            {
                throw new InternalOrdersException("Duplicate stock line item, update the quantity instead.");
            }

            Obj.InternalComment = null;
            db.SaveChanges();

            return Obj.Id;
        }

        public static int editInternalOrderDraft(int key, DAL.DTO.InternalOrder values)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.InternalOrders.FirstOrDefault(o => o.Id == key);
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            DTO.InternalOrder changes = new DTO.InternalOrder();
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), changes, serializerSettings);

            if (changes.internalOrderItems != null)
            {
                db.InternalOrderItems.RemoveRange(Obj.InternalOrderItems);
                Obj.InternalOrderItems.Clear();
            }

            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);

            if (Obj.InternalOrderItems.GroupBy(x => x.StockId).Any(g => g.Count() > 1))
            {
                throw new InternalOrdersException("Duplicate stock line item, update the quantity instead.");
            }

            if (Obj.DeliveryDate.ToString() == "0001/01/01 00:00:00")
            {
                Obj.DeliveryDate = DateTime.Now;
            }

            if (Obj.FollowUpDate.ToString() == "0001/01/01 00:00:00")
            {
                Obj.FollowUpDate = DateTime.Now;
            }

            Obj.DateCreated = DateTime.Now;

            Obj.InternalComment = null;
            db.SaveChanges();

            return Obj.Id;
        }
        public static int validateInternalOrder(int id, string Username)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            if(Username == "" || Username == null)
            {
                Username = "Unknown";
            }

            var Obj = db.InternalOrders.Find(id);

            if (Obj.FollowUpDate > Obj.DeliveryDate)
            {
                throw new InternalOrdersException("The Follow Up Date cannot be after the Delivery Date.");
            }

            Obj.StatusId = (int)DAL.Constants.InternalOrderStatus.PENDING;

            foreach (var item in Obj.InternalOrderItems)
            {
                var PriceLookUp = item.Stock.PriceLookups.FirstOrDefault();
                var StockMonitored = item.Stock.Monitored;

                var supplier = db.Suppliers.FirstOrDefault(s => s.Id == Obj.SupplierId);
                var stock = db.Stocks.FirstOrDefault(s => s.Id == item.StockId);

                if (PriceLookUp == null)
                {                   

                    if (StockMonitored)
                    {
                        PriceLookUp = new Models.PriceLookup
                        {
                            Price = 0,
                            Comment = "New price waiting approval",
                            SupplierId = (int)Obj.SupplierId,
                            StockId = item.StockId,
                            Date = DateTime.Now
                        };

                        PriceLookUp.PriceIncreases.Add(new Models.PriceIncrease
                        {
                            IncreaseTypeId = (int)DAL.Constants.IncreaseTypes.NEWPRICE,
                            Comment = "Price Changed via Internal Order Item.",
                            Date = DateTime.Now,
                            Increase = item.Value,
                            IncreaseOrigin = "via internal order"
                        });
                        Obj.StatusId = (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL;

                        var log = new DAL.Models.PriceLookupLog
                        {
                            Stock = stock.Code + " - " + stock.ProductName,
                            OldPrice = 0,
                            NewPrice = item.Value,
                            Date = (DateTime)Obj.DateCreated,
                            Comment = Obj.Comment,
                            Uom = stock.Uom.Name,
                            Application = "not yet set",
                            Supplier = supplier.CompanyName,
                            //TODO add username
                            Username = Username,
                            InternalProductName = stock.InternalProductName,
                            Currency = supplier.Currency.Iso
                        };
                        db.PriceLookupLogs.Add(log);
                        
                    }
                    else
                    {
                        PriceLookUp = new Models.PriceLookup
                        {
                            Price = item.Value,
                            Comment = "Price Changed via Internal Order Item.",
                            SupplierId = (int)Obj.SupplierId,
                            StockId = item.StockId,
                            Date = DateTime.Now
                        };

                        var log = new DAL.Models.PriceLookupLog
                        {
                            Stock = stock.Code + " - " + stock.InternalProductName,
                            OldPrice = 0,
                            NewPrice = item.Value,
                            Date = (DateTime)Obj.DateCreated,
                            Comment = Obj.Comment,
                            Uom = stock.Uom.Name,
                            Application = "not yet set",
                            Supplier = supplier.CompanyName,
                            //TODO add username
                            Username = Username,
                            InternalProductName = stock.InternalProductName,
                            Currency = supplier.Currency.Iso
                        };

                        db.PriceLookupLogs.Add(log);
                    }

                    item.Stock.PriceLookups.Add(PriceLookUp);

                }
                else if (PriceLookUp.Price != item.Value)
                {
                    if (StockMonitored)
                    {
                        if (PriceLookUp.PriceIncreases.FirstOrDefault() != null)
                        {
                            db.PriceIncreases.Remove(PriceLookUp.PriceIncreases.First());
                            PriceLookUp.PriceIncreases.Clear();
                        }


                        PriceLookUp.PriceIncreases.Add(new Models.PriceIncrease
                        {
                            IncreaseTypeId = (int)DAL.Constants.IncreaseTypes.NEWPRICE,
                            Comment = "Price Changed via Internal Order Item.",
                            Date = DateTime.Now,
                            Increase = item.Value,
                            IncreaseOrigin = "via internal order"
                        });
                        Obj.StatusId = (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL;

                        var log = new DAL.Models.PriceLookupLog
                        {
                            Stock = stock.Code + " - " + stock.InternalProductName,
                            OldPrice = PriceLookUp.Price,
                            NewPrice = item.Value,
                            Date = (DateTime)Obj.DateCreated,
                            Comment = Obj.Comment,
                            Uom = stock.Uom.Name,
                            Application = "not yet set",
                            Supplier = supplier.CompanyName,
                            //TODO add username
                            Username = Username,
                            InternalProductName = stock.InternalProductName,
                            Currency = supplier.Currency.Iso
                        };
                        db.PriceLookupLogs.Add(log);
                    }
                    else
                    {
                        PriceLookUp.Price = item.Value;
                        PriceLookUp.SupplierId = (int)Obj.SupplierId;
                        PriceLookUp.StockId = item.StockId;
                        PriceLookUp.Date = DateTime.Now;
                        PriceLookUp.Comment = "Price Changed via Internal Order Item.";

                        var log = new DAL.Models.PriceLookupLog
                        {
                            Stock = stock.Code + " - " + stock.InternalProductName,
                            OldPrice = PriceLookUp.Price,
                            NewPrice = item.Value,
                            Date = (DateTime)Obj.DateCreated,
                            Comment = Obj.Comment,
                            Uom = stock.Uom.Name,
                            Application = "not yet set",
                            Supplier = supplier.CompanyName,
                            //TODO add username
                            Username = Username,
                            InternalProductName = stock.InternalProductName,
                            Currency = supplier.Currency.Iso
                        };
                        db.PriceLookupLogs.Add(log);
                    }

                }
            }

            db.SaveChanges();
            return Obj.Id;
        }

        public static int editApproveOrder(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.InternalOrders.Where(i => i.Id == id).FirstOrDefault();
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            Obj.StatusId = (int)Constants.InternalOrderStatus.APPROVED;
            Obj.DateApproved = DateTime.Now;
            Obj.InternalComment = null;

            
            if(Obj.Vat > 0)
            {
                //add vat to the vatGL code
                var glid = db.Glcodes.FirstOrDefault(s => s.AssignVat == true).Id;
                var previousVat = db.VatStoreds.FirstOrDefault()?.VatAmount;
                if(previousVat == null)
                {
                    previousVat = 0;
                }

                var vatStored = new Models.VatStored
                {
                    GlcodeId = glid,
                    VatAmount = (decimal)previousVat + (decimal)Obj.Vat
                };
                db.VatStoreds.Update(vatStored);
            }
         
            
            //store internal order to get grn number
            var latestEntry = new DAL.Models.LatestGrn
            {
                InternalOrderId = Obj.Id,
                GrnIncrement = 0
            };
            db.LatestGrns.Add(latestEntry);
            db.SaveChanges();
            //return 1
            return Obj.StatusId;
        }

        public static int editDenyOrder(DAL.DTO.InternalOrderAction action)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.InternalOrders.Where(i => i.Id == action.Id).FirstOrDefault();
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            Obj.StatusId = (int)Constants.InternalOrderStatus.DENIED;
            Obj.DateApproved = DateTime.Now;
            Obj.InternalComment = action.InternalComment;
            db.SaveChanges();
            //return 3
            return Obj.StatusId;
        }

        public static void removeDenyOrder(int Id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.InternalOrders.Where(i => i.Id == Id).FirstOrDefault();
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            db.OnceOffItems.RemoveRange(Obj.OnceOffItems);
            db.InternalOrderItems.RemoveRange(Obj.InternalOrderItems);
            db.Services.RemoveRange(Obj.Services);

            db.InternalOrders.Remove(Obj);
            db.SaveChanges();
        }

        public static int editReviewOrder(DAL.DTO.InternalOrderAction action)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.InternalOrders.Where(i => i.Id == action.Id).FirstOrDefault();
            if (Obj == null) throw new InternalOrdersException("Internal Order does not exist.");

            Obj.StatusId = (int)Constants.InternalOrderStatus.REVIEW;
            Obj.InternalComment = action.InternalComment;
            Obj.DateApproved = DateTime.Now;
            db.SaveChanges();
            //return 5
            return Obj.StatusId;
        }

        public static DAL.DTO.InternalOrder GetEmailData(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   PlacedByFullName = p.PlacedBy.Name + " " + p.PlacedBy.Surname,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,               
                   ApproveById = p.ApproveById,
                   ApproveByFullName = p.ApproveBy.Name + " " + p.ApproveBy.Surname,
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
                   PlantLocationName = p.PlantLocation.Name,
                   SupplierComment = p.SupplierComment,
                   SupplierCurrency = p.Supplier.Currency.Iso,
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   ProjectName = p.Project.Name,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
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
                       UomName = c.Uom.Name,
                       PackSize = c.PackSize,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               }).First(d => d.Id == id);
            return source;
        }

        public static string GetPlacedByRecipient(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int recipientId = db.InternalOrders.Where(s => s.Id == id).Select(t => t.PlacedById).FirstOrDefault();
            string email = db.UserDetails.Where(i => i.UserId == recipientId).Select(e => e.Email).FirstOrDefault();
            return email;
        }

        public static string GetRecipient(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int recipientId = db.InternalOrders.Where(s => s.Id == id).Select(t => t.RequestedById).FirstOrDefault();
            string email = db.UserDetails.Where(i => i.UserId == recipientId).Select(e => e.Email).FirstOrDefault();
            return email;
        }

        public static string GetApprovedRecipient(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int recipientId = (int)db.InternalOrders.Where(s => s.Id == id).Select(t => t.ApproveById).FirstOrDefault();
            string email = db.UserDetails.Where(i => i.UserId == recipientId).Select(e => e.Email).FirstOrDefault();
            return email;
        }

        public static int GetOrderStatus(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            int statusId = (int)db.InternalOrders.Where(s => s.Id == id).Select(t => t.StatusId).FirstOrDefault();
            int orderStatus = statusId == (int)DAL.Constants.InternalOrderStatus.APPROVED  || statusId == (int)DAL.Constants.InternalOrderStatus.DENIED  || statusId == (int)DAL.Constants.InternalOrderStatus.REVIEW ? statusId: -1;
            return orderStatus;
        }
        public static List<DAL.DTO.InternalOrderItem> getPriceIncreaseItemList(DAL.DTO.InternalOrder values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            List<DAL.DTO.InternalOrderItem> increasedItems = new List<DTO.InternalOrderItem>();
            foreach (var item in values.internalOrderItems)
            {
                var Obj = db.Stocks.Where(s => s.Id == item.StockId).FirstOrDefault();
                var StockMonitored = Obj.Monitored;
                item.InternalProductName = Obj.InternalProductName;
                if (StockMonitored)
                {
                    if (item.Value > item.OriginalValue)
                    {
                        increasedItems.Add(item);
                    }
                }
            }

            return increasedItems;
        }

        //Create GRN - Select PO
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedGRNInternalOrder()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED)
               .Where(b => b.InternalOrderItems.Sum(s => s.Quantity) > db.Grnitems.Where(d => d.Grn.InternalOrderId == b.Id).Sum(q => q.Quantity) || b.OnceOffItems.Sum(s => s.Quantity) > db.GrnonceOffItems.Where(d => d.Grn.InternalOrderId == b.Id).Sum(q => q.Quantity))               
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApprovedIOSupplierName = p.Id + " - " + p.Supplier.CompanyName,
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
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               });

            return source;
        }

        //Create Invoice - Select PO
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrder()
        {          
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
               .Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED)
               .Where(b => (b.Services.Sum(s => s.Quantity) > db.InvoiceServiceItems.Where(i => i.Service.InternalOrderId == b.Id).Sum(q => q.Quantity)) 
               || (b.Grns.Count > 0 && b.InternalOrderItems.Sum(q => q.Quantity) > db.InvoiceItems.Where(s => s.Grnitem.InternalOrderItem.InternalOrderId == b.Id).Sum(q => q.Quantity))
               || (b.Grns.Count > 0 && b.OnceOffItems.Sum(g => g.Quantity) > db.InvoiceOnceOffItems.Where(t => t.GrnonceOffItem.OnceOffItems.InternalOrderId == b.Id).Sum(q => q.Quantity)))
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApprovedIOSupplierName = p.Id + " - " + p.Supplier.CompanyName,
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
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               });

            return source;
        }

        //TODO: Mel Temp fix for Lu on Live
        //Create Invoice - Select PO
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderAppClose()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
               .Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED || a.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE)
               .Where(b => (b.Services.Sum(s => s.Quantity) > db.InvoiceServiceItems.Where(i => i.Service.InternalOrderId == b.Id).Sum(q => q.Quantity))
               || (b.Grns.Count > 0 && b.InternalOrderItems.Sum(q => q.Quantity) > db.InvoiceItems.Where(s => s.Grnitem.InternalOrderItem.InternalOrderId == b.Id).Sum(q => q.Quantity))
               || (b.Grns.Count > 0 && b.OnceOffItems.Sum(g => g.Quantity) > db.InvoiceOnceOffItems.Where(t => t.GrnonceOffItem.OnceOffItems.InternalOrderId == b.Id).Sum(q => q.Quantity)))
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApprovedIOSupplierName = p.Id + " - " + p.Supplier.CompanyName,
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
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize,
                       UomPrice = c.UomPrice
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               });

            return source;
        }

        //Edit/Dlt GRN - Select PO
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderGrn()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.InternalOrders
               .Where(a => a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED && a.Grns.Count > 0)               
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApprovedIOSupplierName = p.Id + " - " + p.Supplier.CompanyName,
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
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               });
            return source;
        }

        //Edit/Dlt Invoice - Select PO
        public static IQueryable<DAL.DTO.InternalOrder> getApprovedInternalOrderInvoice()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.InternalOrders
               .Where(a => (a.StatusId == (int)DAL.Constants.InternalOrderStatus.APPROVED && a.Invoices.Count > 0) || (a.StatusId == (int)DAL.Constants.InternalOrderStatus.CLOSE && a.Invoices.Count > 0))
               .Select(p => new DAL.DTO.InternalOrder
               {
                   Id = p.Id,
                   RequestedById = p.RequestedById,
                   RequestByFullName = p.RequestedBy.Name + " - " + p.RequestedBy.Surname,
                   PlacedById = p.PlacedById,
                   QuotationNumber = p.QuotationNumber,
                   SupplierId = p.SupplierId,
                   SupplierFullName = p.Supplier.CompanyName,
                   ApprovedIOSupplierName = p.Id + " - " + p.Supplier.CompanyName,
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
                   Vat = p.Vat,
                   ProjectId = p.ProjectId,
                   CompanyId = p.CompanyId,
                   CompanyName = p.Company.Name,
                   internalOrderItems = p.InternalOrderItems.Select(c => new DAL.DTO.InternalOrderItem
                   {
                       Id = c.Id,
                       InternalOrderId = c.InternalOrderId,
                       StockId = c.StockId,
                       OriginalValue = c.OriginalValue,
                       Value = c.Value,
                       Quantity = c.Quantity,
                       GlcodeId = c.GlcodeId,
                       DepartmentId = c.DepartmentId,
                       Uomid = c.Uomid,
                       Total = c.Total,
                       VatAppl = c.VatAppl,
                       GrnAppl = c.GrnAppl,
                       TotalUnits = c.Quantity * c.PackSize
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
                       GlFullName = f.Glcode != null ? (f.Glcode.Code + " - " + f.Glcode.Name) : null,
                       TotalUnits = (int)(f.Quantity != null ? f.Quantity : 0) * (int)(f.PackSize != null ? f.PackSize : 0),
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
                       GlFullName = s.Glcode != null ? (s.Glcode.Code + " - " + s.Glcode.Name) : null,
                       DepartmentId = s.DepartmentId,
                       DepartmentName = s.Department.Name,
                       Quantity = s.Quantity,
                       Code = s.Code,
                       Total = s.Quantity * s.Value
                   }).ToList()
               });
            return source;
        }        

        public static Object GetMonitoredItems(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            List<DAL.DTO.PriceIncrease> priceincreaselist = new List<DAL.DTO.PriceIncrease>();

            var Obj = db.InternalOrders.Find(id);
            foreach (var item in Obj.InternalOrderItems)
            {
                var PriceLookUp = item.Stock.PriceLookups.FirstOrDefault();
                var StockMonitored = item.Stock.Monitored;

                if(PriceLookUp != null)
                {
                    if (StockMonitored)
                    {
                        priceincreaselist.AddRange(
                            PriceLookUp.PriceIncreases.Select(n => new DAL.DTO.PriceIncrease
                            {
                                Id = n.Id,
                                IncreaseTypeId = n.IncreaseTypeId,
                                IncreaseTypeName = n.IncreaseType.Type,
                                Comment = n.Comment,
                                Date = n.Date,
                                Increase = n.Increase,
                                PriceLookUpId = n.PriceLookUpId,
                                IncreaseOrigin = n.IncreaseOrigin
                            }).ToList()
                        );
                    }
                }
                
            }
            return priceincreaselist;
        }

        public static List<string> GetPriceIncreaseReminder()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var users = db.UserPermissions.Where(p => p.PermissionId == (int)Constants.Permissions.APPROVEINCREASES).Select(u => u.UserId).ToList();
            List<string> emails = new List<string>();

            if(users.Count > 0)
            {
                foreach (var user in users)
                {
                    var useremail = db.UserDetails.Where(i => i.UserId == user).Select(e => e.Email).FirstOrDefault();
                    emails.Add(useremail);
                }
            }
           
            return emails;
        }

        public static Object GetStockItems(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            List<DAL.DTO.PriceIncrease> priceincreaselist = new List<DAL.DTO.PriceIncrease>();

            var Obj = db.InternalOrders.Find(id);
            foreach (var item in Obj.InternalOrderItems)
            {
                var PriceLookUp = item.Stock.PriceLookups.FirstOrDefault();
                //var StockMonitored = item.Stock.Monitored;

                if (PriceLookUp != null)
                {
                    //if (StockMonitored)
                    //{
                        priceincreaselist.AddRange(
                            PriceLookUp.PriceIncreases.Select(n => new DAL.DTO.PriceIncrease
                            {
                                Id = n.Id,
                                IncreaseTypeId = n.IncreaseTypeId,
                                IncreaseTypeName = n.IncreaseType.Type,
                                Comment = n.Comment,
                                Date = n.Date,
                                Increase = n.Increase,
                                PriceLookUpId = n.PriceLookUpId,
                                IncreaseOrigin = n.IncreaseOrigin
                            }).ToList()
                        );
                    //}
                }

            }
            return priceincreaselist;
        }

        public static List<string> GetInvoiceIncreaseReminder()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var users = db.UserPermissions.Where(p => p.PermissionId == (int)Constants.Permissions.INVOICEINCREASES).Select(u => u.UserId).ToList();
            List<string> emails = new List<string>();

            if (users.Count > 0)
            {
                foreach (var user in users)
                {
                    var useremail = db.UserDetails.Where(i => i.UserId == user).Select(e => e.Email).FirstOrDefault();
                    emails.Add(useremail);
                }
            }

            return emails;
        }

        public static int getDefaultCompany()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Companies.Where(d => d.MotherCompany == true).FirstOrDefault().Id;
            return source;
        }
    }
}
