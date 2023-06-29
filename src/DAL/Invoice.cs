using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class Invoice
    {
        //getinvoice for drodowns
        public static IQueryable<DAL.DTO.Invoice> getInvoice()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.Invoices
               .Select(p => new DAL.DTO.Invoice
               {
                   Id = p.Id,
                   InvoiceNumber = p.InvoiceNumber,
                   Total = p.Total,
                   InternalOrderId = p.InternalOrderId,
                   DateCreated = p.DateCreated,                   
               });
            return source;
        }

        //popup information get received grn items and invoicable items
        public static DAL.DTO.InvoiceableItems GetInvoiceableItems(int internalOrderId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var data = new DAL.DTO.InvoiceableItems();
            var IO = db.InternalOrders.Find(internalOrderId);
         
            data.InvoiceableServices = IO.Services.Where(s => s.InvoiceServiceItems.Count == 0).Select(s => new DAL.DTO.Service
            {
                Id = s.Id,
                Description = s.Description,
                Quantity =  s.Quantity
            }).ToList();

            data.InvoiceableGrnItems = IO.Grns.Where(a => a.GrnonceOffItems.Any(b => b.InvoiceOnceOffItems.Count == 0) || a.Grnitems.Any(b => b.InvoiceItems.Count == 0)).Select(c => new DAL.DTO.InvoiceableGrnItems
            {
                GrnNumber = c.GrnNumber,
                DateCreated = c.DateCreated,
                Comment = c.Comment,
                GrnonceOffItems = c.GrnonceOffItems.Where(d => d.InvoiceOnceOffItems.Count == 0).Select(e => new DAL.DTO.GrnonceOffItem
                {
                    Id = e.Id,
                    Description = e.OnceOffItems.Description,
                    Quantity = e.Quantity,
                    ReceivedQuantity = e.Grn.GrnonceOffItems.Sum(s => s.Quantity),
                    Comment = e.Comment
                }).ToList(),
                 GrnItems = c.Grnitems.Where(d => d.InvoiceItems.Count == 0).Select(e => new DAL.DTO.Grnitem
                 {
                     Id = e.Id,
                     ManufacturerProductName = e.InternalOrderItem.Stock.ProductName + " - " + e.InternalOrderItem.Stock.InternalProductName,
                     Quantity = e.Quantity,
                     ReceivedQuantity = e.Grn.Grnitems.Sum(s => s.Quantity),
                     Comment = e.Comment,
                 }).ToList()

            }).ToList();
            return data;
        }

        //add invoice to internal order sending in internal order number
        public static DAL.DTO.InvoiceOrderData GetNewInvoiceInternalOrderData(DAL.DTO.ToBeInvoicedItems ToBeInvoicedItems)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.InvoiceOrderData data = new DAL.DTO.InvoiceOrderData();

            var IO = db.InternalOrders.Find(ToBeInvoicedItems.InternalOrderId);

            data.InternalOrder = new DAL.DTO.InternalOrder
            {
                Id = IO.Id,
                RequestByFullName = IO.RequestedBy.Name + " - " + IO.RequestedBy.Surname,
                PlacedByFullName = IO.PlacedBy.Name + " - " + IO.PlacedBy.Surname,
                SupplierFullName = IO.Supplier.CompanyName,
                Comment = IO.Comment,
                InternalComment = IO.Comment,
                SupplierComment = IO.SupplierComment,
                DateCreated = IO.DateCreated,
                DateApproved = IO.DateApproved,
                Vat = IO.Vat,
                Total = IO.Total,
                SupplierCurrency = IO.Supplier.Currency.Iso
            };

            data.Invoice = new DAL.DTO.Invoice
            {
                InternalOrderId = data.InternalOrder.Id,
                //InvoiceItems = new List<DTO.InvoiceItem>(),
                ExpectedTotal = 0,// (decimal)(data.InternalOrder.Total - IO.Invoices.Sum(t => t.Total))
                Vat=0
            };

            data.Invoice.InvoiceItems = db.Grnitems?.Where(h => h.Grn.InternalOrderId == ToBeInvoicedItems.InternalOrderId)
                .Where(g => ToBeInvoicedItems.ListedItem.Contains(g.Id)).Select(i => new DAL.DTO.InvoiceItem
                {
                    ManufacturerCode = i.InternalOrderItem.Stock.Code,//i.Stock.Code,
                    ManufacturerProductName = i.InternalOrderItem.Stock.ProductName + " - " + i.InternalOrderItem.Stock.InternalProductName,
                    ProductName = i.InternalOrderItem.Stock.ProductName,
                    InternalProductName = i.InternalOrderItem.Stock.InternalProductName,
                    ItemValue = i.InternalOrderItem.Value,
                    Quantity = i.Quantity,
                    ReceivedQuantity = db.InvoiceItems.Where(e => e.Grnitem.InternalOrderItemId == i.InternalOrderItemId).Sum(s => s.Quantity),
                    RequiredQuantity = i.InternalOrderItem.Quantity,//i.Quantity,
                    GrnitemId = i.Id,
                    UomName = i.InternalOrderItem.Uom.Name,
                    PackSize = i.InternalOrderItem.Stock.PackSize,
                    GlcodeId = i.InternalOrderItem.GlcodeId,
                    DepartmentId = i.InternalOrderItem.DepartmentId,
                    VatAppl = i.InternalOrderItem.VatAppl,
                    GrnQtyTotal = i.Grn.Grnitems.Where(e => e.InternalOrderItem.InternalOrderId == data.InternalOrder.Id).Sum(s => s.Quantity)
                })?.ToList();

            //copy as above
            data.Invoice.InvoiceOnceOffItems = db.GrnonceOffItems?.Where(h => h.Grn.InternalOrderId == ToBeInvoicedItems.InternalOrderId)
                .Where(g => ToBeInvoicedItems.OnceOffItem.Contains(g.Id)).Select(o => new DAL.DTO.InvoiceOnceOffItem
                {
                    Description = o.OnceOffItems.Description,
                    ItemValue = o.OnceOffItems.Value,
                    Quantity = o.Quantity,
                    ReceivedQuantity = o.InvoiceOnceOffItems.Where(e => e.GrnonceOffItem.OnceOffItemsId == o.OnceOffItemsId).Sum(s => s.Quantity),//o.Quantity,//o.Grn.GrnonceOffItems.Sum(s => s.Quantity),
                    RequiredQuantity = o.OnceOffItems.Quantity,//o.Quantity,
                    GrnonceOffItemId = o.Id,
                    GlcodeId = o.OnceOffItems.GlcodeId,
                    UomName = o.OnceOffItems.Uom.Name,
                    PackSize = o.OnceOffItems.PackSize,
                    VatAppl = o.OnceOffItems.VatAppl,
                    DepartmentId = o.OnceOffItems.DepartmentId,
                    Code = o.OnceOffItems.Code,
                    GrnQtyTotal = o.Grn.GrnonceOffItems.Where(e => e.OnceOffItems.InternalOrderId == data.InternalOrder.Id).Sum(s => s.Quantity)
                })?.ToList();

            data.Invoice.InvoiceServiceItems = IO.Services?.Where(g => ToBeInvoicedItems.Service.Contains(g.Id))
                .Select(s => new DAL.DTO.InvoiceServiceItem
                {
                    Description = s.Description,
                    Quantity = (int)s.Quantity,
                    RequiredQuantity = (int)s.Quantity,                
                    ItemValue = s.Value,
                    ReceivedQuantity = s.InvoiceServiceItems.Sum(s => s.Quantity),
                    ServiceId = s.Id,
                    GlcodeId = s.GlcodeId,
                    VatAppl = s.VatAppl,
                    DepartmentId = s.DepartmentId,
                    Code = s.Code
                })?.ToList();

            return data;
        }

        //edit invoice by sending in the invoice number
        public static DAL.DTO.InvoiceOrderData GetInvoiceData(int invoiceId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.InvoiceOrderData data = new DAL.DTO.InvoiceOrderData();

            var invoice = db.Invoices.Find(invoiceId);

            data.InternalOrder = new DAL.DTO.InternalOrder
            {
                Id = invoice.InternalOrder.Id,
                RequestByFullName = invoice.InternalOrder.RequestedBy.Name + " - " + invoice.InternalOrder.RequestedBy.Surname,
                PlacedByFullName = invoice.InternalOrder.PlacedBy.Name + " - " + invoice.InternalOrder.PlacedBy.Surname,
                SupplierFullName = invoice.InternalOrder.Supplier.CompanyName,
                Comment = invoice.InternalOrder.Comment,
                InternalComment = invoice.InternalOrder.Comment,
                SupplierComment = invoice.InternalOrder.SupplierComment,
                DateCreated = invoice.InternalOrder.DateCreated,
                DateApproved = invoice.InternalOrder.DateApproved,
                Vat = invoice.InternalOrder.Vat,
                Total = invoice.InternalOrder.Total,
                SupplierCurrency = invoice.InternalOrder.Supplier.Currency.Iso
            };

            data.Invoice = new DAL.DTO.Invoice
            {
                Id = invoice.Id,
                InvoiceNumber = invoice.InvoiceNumber,
                Total = invoice.Total,
                InternalOrderId = invoice.InternalOrder.Id,
                DateCreated = invoice.DateCreated,
                InvoiceItems = invoice.InvoiceItems.Select(i => new DAL.DTO.InvoiceItem
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    ItemValue = i.ItemValue,
                    ManufacturerCode = i.Grnitem.InternalOrderItem.Stock.Code,
                    ManufacturerProductName = i.Grnitem.InternalOrderItem.Stock.ProductName + " - " + i.Grnitem.InternalOrderItem.Stock.InternalProductName,
                    ProductName = i.Grnitem.InternalOrderItem.Stock.ProductName,
                    InternalProductName = i.Grnitem.InternalOrderItem.Stock.InternalProductName,
                    RequiredQuantity = i.Grnitem.InternalOrderItem.Quantity,
                    ReceivedQuantity = db.InvoiceItems.Where(e => e.Grnitem.InternalOrderItemId == i.Grnitem.InternalOrderItemId).Sum(s => s.Quantity) - i.Quantity,
                    GrnitemId = i.Grnitem.InternalOrderItemId,
                    UomName = i.Grnitem.InternalOrderItem.Uom.Name,
                    PackSize = i.Grnitem.InternalOrderItem.Stock.PackSize,
                    GlcodeId = i.Grnitem.InternalOrderItem.GlcodeId,
                    DepartmentId = i.Grnitem.InternalOrderItem.DepartmentId,
                    VatAppl = i.Grnitem.InternalOrderItem.VatAppl,
                    InvoicePrice = i.InvoicePrice,
                    GrnQtyTotal = i.Quantity
                }).ToList(),

                InvoiceOnceOffItems = invoice.InvoiceOnceOffItems.Select(i => new DAL.DTO.InvoiceOnceOffItem
                {
                    Id = i.Id,
                    Quantity = i.Quantity,
                    ItemValue = i.ItemValue,
                    Description = i.GrnonceOffItem.OnceOffItems.Description,
                    RequiredQuantity = i.GrnonceOffItem.OnceOffItems.Quantity,
                    ReceivedQuantity = db.InvoiceOnceOffItems.Where(e => e.GrnonceOffItem.OnceOffItemsId == i.GrnonceOffItem.OnceOffItemsId).Sum(s => s.Quantity) - i.Quantity,//i.GrnonceOffItem.InvoiceOnceOffItems.Sum(s => s.Quantity) - i.Quantity,
                    GrnonceOffItemId = i.GrnonceOffItemId,
                    UomName = i.GrnonceOffItem.OnceOffItems.Uom.Name,
                    PackSize = i.GrnonceOffItem.OnceOffItems.PackSize,
                    GlcodeId = i.GrnonceOffItem.OnceOffItems?.GlcodeId,
                    VatAppl = i.GrnonceOffItem.OnceOffItems.VatAppl,
                    InvoicePrice = i.InvoicePrice,
                    DepartmentId = i.GrnonceOffItem.OnceOffItems.DepartmentId,
                    Code = i.GrnonceOffItem.OnceOffItems.Code,
                    GrnQtyTotal = i.Quantity
                }).ToList(),

                InvoiceServiceItems = invoice.InvoiceServiceItems.Select(s => new DAL.DTO.InvoiceServiceItem
                {
                    Description = s.Service.Description,
                    ItemValue = s.Service.Value,
                    Quantity = s.Quantity,
                    RequiredQuantity = (int)s.Service.Quantity,
                    ReceivedQuantity = s.Service.InvoiceServiceItems.Sum(s => s.Quantity) - s.Quantity,
                    ServiceId = s.Id,
                    GlcodeId = s.Service.GlcodeId,
                    VatAppl = s.Service.VatAppl,
                    InvoicePrice = s.InvoicePrice,
                    DepartmentId = s.Service.DepartmentId,
                    Code = s.Service.Code
                }).ToList(),

                //ExpectedTotal = (decimal)(data.InternalOrder.Total - invoice.InternalOrder.Invoices.Sum(t => t.Total))
            };

            data.Invoice.AllItemsReceived = data.Invoice.InvoiceItems.Sum(q => q.RequiredQuantity - q.ReceivedQuantity - q.Quantity) == 0;

            return data;
        }

        //adding a new invoice to an internal order
        public static int AddInvoice(DAL.DTO.Invoice invoice)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Invoice();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(invoice), Obj);

            var check = db.Invoices.Where(i => i.InvoiceNumber == invoice.InvoiceNumber).FirstOrDefault();
            if (check != null)
            {
                throw new InvoiceException("Invoice already exists.");
            }            

            Obj.DateCreated = DateTime.Now;            
            db.Invoices.Add(Obj);
            db.SaveChanges();
            var Invoiceid = Obj.Id;
            return Invoiceid;
        }

        //editing a existing invoice
        public static int UpdateInvoice(DAL.DTO.Invoice invoice)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Invoices.FirstOrDefault(o => o.Id == invoice.Id);

          /*  var serializerSettings = new JsonSerializerSettings();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(invoice), Obj, serializerSettings);*/
            var check = db.Invoices.Where(i => i.InvoiceNumber == invoice.InvoiceNumber && i.Id != invoice.Id).FirstOrDefault();
            if (check != null)
            {
                throw new InvoiceException("Invoice already exists.");
            }

            Obj.InvoiceNumber = invoice.InvoiceNumber;

            foreach (var item in invoice.InvoiceItems)
            {
                var itemObj = Obj.InvoiceItems.Where(i => i.Id == item.Id).First();
                itemObj.InvoicePrice = item.InvoicePrice;
            }

            foreach (var item in invoice.InvoiceOnceOffItems)
            {
                var itemObj = Obj.InvoiceOnceOffItems.Where(i => i.Id == item.Id).First();
                itemObj.InvoicePrice = item.InvoicePrice;
            }

            foreach (var item in invoice.InvoiceServiceItems)
            {
                var itemObj = Obj.InvoiceServiceItems.Where(i => i.Id == item.Id).First();
                itemObj.InvoicePrice = item.InvoicePrice;
            }
            db.SaveChanges();

            var Invoiceid = Obj.Id;
            return Invoiceid;
            /*return invoice.Id;*/
        }

        //delete an invoice by sending in the invoice number
        public static int RemoveInvoice(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var invoiceObj = db.Invoices.FirstOrDefault(s => s.Id == id);
            if (invoiceObj == null) throw new InvoiceException("Invoice does not exist.");

            db.InvoiceItems.RemoveRange(invoiceObj.InvoiceItems);
            db.InvoiceOnceOffItems.RemoveRange(invoiceObj.InvoiceOnceOffItems);
            db.InvoiceServiceItems.RemoveRange(invoiceObj.InvoiceServiceItems);

            db.Invoices.Remove(invoiceObj);
            db.SaveChanges();

            return id;
        }

        public static List<DAL.DTO.Grn> GetCapturedGrnItems(int internalOrderId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var data = db.Grns.Where(g => g.InternalOrderId == internalOrderId)
                .Select(i => new DAL.DTO.Grn
                {
                    Id = i.Id,
                    GrnNumber = i.GrnNumber,
                    DateCreated = i.DateCreated,
                    Comment = i.Comment,
                    GrnItems = i.Grnitems.Select(j => new DAL.DTO.Grnitem
                    {
                        Id = j.Id,
                        ManufacturerProductName = j.InternalOrderItem.Stock.ProductName + " - " + j.InternalOrderItem.Stock.InternalProductName,
                        ReceivedQuantity = j.Grn.Grnitems.Sum(q => q.Quantity),
                        Comment = j.Comment
                    }).ToList(),
                    GrnonceOffItems = i.GrnonceOffItems.Select(j => new DAL.DTO.GrnonceOffItem
                    {
                        Id = j.Id,
                        Description = j.OnceOffItems.Description,
                        ReceivedQuantity = j.Grn.GrnonceOffItems.Sum(q => q.Quantity),
                        Comment = j.Comment
                    }).ToList()
                }).ToList();
            
            return data;
        }
    }
}
