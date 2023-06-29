using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public static class Grn
    {
        //getGrn for drodowns
        public static IQueryable<DAL.DTO.Grn> getGrn()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.Grns
               .Select(p => new DAL.DTO.Grn
               {
                   Id = p.Id,
                   GrnNumber = p.GrnNumber,
                   Total = p.Total,
                   InternalOrderId = p.InternalOrderId,
                   DateCreated = p.DateCreated,
                   CapturerId = p.CapturerId,
                   Comment = p.Comment,
                   EditorId = p.EditorId
               });
            return source;
        }

        //add grn to internal order sending in internal order number
        public static DAL.DTO.GrnOrderData GetNewGrnInternalOrderData(int internalOrderId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.GrnOrderData data = new DAL.DTO.GrnOrderData();

            var IO = db.InternalOrders.Find(internalOrderId);


            data.InternalOrder = new DAL.DTO.InternalOrder
            {
                Id = IO.Id,
                RequestByFullName = IO.RequestedBy.Name + " - " + IO.RequestedBy.Surname,
                PlacedByFullName = IO.PlacedBy.Name + " - " + IO.PlacedBy.Surname,
                SupplierFullName = IO.Supplier.CompanyName,
                Comment = IO.Comment,
                InternalComment = IO.Comment,
                SupplierComment = IO.SupplierComment,
                DateCreated = (DateTime)IO.DateCreated,
                DateApproved = IO.DateApproved,
                Vat = IO.Vat,
                Total = IO.Total,
                SupplierCurrency = IO.Supplier.Currency.Iso,
                PlantLocationId = IO.PlantLocationId,
            };

            data.Grn = new DAL.DTO.Grn
            {
                InternalOrderId = data.InternalOrder.Id,
                GrnItems = new List<DTO.Grnitem>(),
                ExpectedTotal = (decimal)(data.InternalOrder.Total - IO.Grns.Sum(t => t.Total))
            };

            data.Grn.GrnItems = IO.InternalOrderItems.Select(i => new DAL.DTO.Grnitem
            {
                StockId = i.StockId,
                ManufacturerCode = i.Stock.Code,
                ManufacturerProductName = i.Stock.ProductName + " - " + i.Stock.InternalProductName,
                ReceivedQuantity = i.Grnitems.Sum(s => s.Quantity),
                RequiredQuantity = i.Quantity,
                InternalOrderQuantity = i.Quantity,
                InternalOrderItemId = i.Id,
                UomName = i.Uom.Name,
                PackSize = i.Stock.PackSize,
                StoreLocationId = IO.PlantLocation.DefaultStoreId,
            }).ToList();

            data.Grn.GrnonceOffItems = IO.OnceOffItems.Select(o => new DAL.DTO.GrnonceOffItem
            {
                Description = o.Description,
                ReceivedQuantity = o.GrnonceOffItems.Sum(s => s.Quantity),
                RequiredQuantity = o.Quantity,
                InternalOrderQuantity = o.Quantity,
                GrnAppl = o.GrnAppl,
                OnceOffItemsId = o.Id,
                UomName = o.Uom.Name,
                PackSize = o.PackSize,
                Code = o.Code
            }).Where(x => x.GrnAppl == true).ToList();

            return data;
        }

        //edit grn by sending in the grn number
        public static DAL.DTO.GrnOrderData GetGrnInternalOrderData(int grnId, int action)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.GrnOrderData data = new DAL.DTO.GrnOrderData();

            var grn = db.Grns.Find(grnId);

            data.InternalOrder = new DAL.DTO.InternalOrder
            {
                Id = grn.InternalOrder.Id,
                RequestByFullName = grn.InternalOrder.RequestedBy.Name + " - " + grn.InternalOrder.RequestedBy.Surname,
                PlacedByFullName = grn.InternalOrder.PlacedBy.Name + " - " + grn.InternalOrder.PlacedBy.Surname,
                SupplierFullName = grn.InternalOrder.Supplier.CompanyName,
                Comment = grn.InternalOrder.Comment,
                InternalComment = grn.InternalOrder.Comment,
                SupplierComment = grn.InternalOrder.SupplierComment,
                DateCreated = grn.InternalOrder.DateCreated,
                DateApproved = grn.InternalOrder.DateApproved,
                Vat = grn.InternalOrder.Vat,
                Total = grn.InternalOrder.Total,
                SupplierCurrency = grn.InternalOrder.Supplier.Currency.Iso
            };

            data.Grn = new DAL.DTO.Grn
            {
                Id = grn.Id,
                GrnNumber = grn.GrnNumber,
                Total = grn.Total,
                InternalOrderId = grn.InternalOrder.Id,
                DateCreated = grn.DateCreated,
                CapturerId = grn.CapturerId,
                Comment = grn.Comment,
                EditorId = grn.EditorId,

                GrnItems = grn.Grnitems.Select(i => new DAL.DTO.Grnitem
                {
                    Id = i.Id,
                    StockId = i.InternalOrderItem.StockId,
                    Quantity = i.Quantity,
                    ManufacturerCode = i.InternalOrderItem.Stock.Code,
                    ManufacturerProductName = i.InternalOrderItem.Stock.ProductName + " - " + i.InternalOrderItem.Stock.InternalProductName,
                    RequiredQuantity = i.InternalOrderItem.Quantity,
                    ReceivedQuantity = i.InternalOrderItem.Grnitems.Sum(s => s.Quantity) - i.Quantity,
                    InternalOrderQuantity = i.InternalOrderItem.Quantity,
                    InternalOrderItemId = i.InternalOrderItemId,
                    UomName = i.InternalOrderItem.Uom.Name,
                    PackSize = i.InternalOrderItem.Stock.PackSize,
                    StoreLocationId = i.StoreLocationId,
                    Comment = i.Comment
                }).ToList(),

                GrnonceOffItems = grn.GrnonceOffItems.Select(g => new DAL.DTO.GrnonceOffItem
                {
                    Description = g.OnceOffItems.Description,
                    ReceivedQuantity = g.OnceOffItems.GrnonceOffItems.Sum(s => s.Quantity) - g.Quantity,
                    RequiredQuantity = g.OnceOffItems.Quantity,
                    Quantity = g.Quantity,
                    InternalOrderQuantity= g.OnceOffItems.Quantity,
                    OnceOffItemsId = g.OnceOffItemsId,
                    UomName = g.OnceOffItems.Uom.Name,
                    PackSize = g.OnceOffItems.PackSize,
                    Code = g.OnceOffItems.Code,
                    Id = g.Id,
                    Comment= g.Comment
                }).ToList(),

                ExpectedTotal = (decimal)(data.InternalOrder.Total - grn.InternalOrder.Grns.Sum(t => t.Total))
            };

            var invoice = db.Invoices
                .Where(x => x.InternalOrderId == data.InternalOrder.Id)
                .Where(x => x.InvoiceItems.FirstOrDefault(i => i.Grnitem.GrnId == grn.Id).InvoiceId == x.Id)
                .ToList();

            data.Grn.AllItemsReceived = data.Grn.GrnItems.Sum(q => q.RequiredQuantity - q.ReceivedQuantity - q.Quantity) == 0;

            switch (action)
            {
                case (int)Constants.GrnAction.UPDATE:
                    if (invoice.Count() > 0 && data.Grn.AllItemsReceived)
                    {
                        throw new Exception("Can't edit GRN, Invoice processed for this GRN");
                    }
                    break;
                case (int)Constants.GrnAction.REMOVE:
                    if (invoice.Count() > 0 && data.Grn.AllItemsReceived)
                    {
                        throw new Exception("Can't delete GRN, Invoice processed for this GRN");
                    }
                    break;
                default:
                    break;
            }
            return data;
        }

        //adding a new grn to an internal order
        public static DAL.Models.Grn AddGrn(DAL.DTO.Grn grn)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Grn();
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(grn), Obj);

            var check = db.Grns.Where(i => i.GrnNumber == grn.GrnNumber).FirstOrDefault();
            if (check != null)
            {
                throw new GrnException("GRN already exists.");
            }

            Obj.DateCreated = DateTime.Now;
            db.Grns.Add(Obj);

            var latestGrn = db.LatestGrns.Where(e => e.InternalOrderId == grn.InternalOrderId).FirstOrDefault();
            latestGrn.GrnIncrement = latestGrn.GrnIncrement + 1;

            db.SaveChanges();

            var IO = db.InternalOrders.Where(i => i.Id == grn.InternalOrderId).First();

            var test1 = IO.OnceOffItems.Sum(s => s.Quantity);
            var test2 = IO.OnceOffItems.Sum(i => i.GrnonceOffItems.Sum(x => x.Quantity));


            if (IO.OnceOffItems.Sum(s => s.Quantity) == IO.OnceOffItems.Sum(i => i.GrnonceOffItems.Sum(x => x.Quantity)) 
                && IO.InternalOrderItems.Sum(s => s.Quantity) == IO.InternalOrderItems.Sum(i => i.Grnitems.Sum(x => x.Quantity))){

                IO.StatusId = (int)DAL.Constants.InternalOrderStatus.CLOSE;                
                db.SaveChanges();
            }

            return Obj;
        }

        //editing a existing grn
        public static DAL.Models.Grn UpdateGrn(DAL.DTO.Grn grn)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Grns.FirstOrDefault(o => o.Id == grn.Id);

            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(grn), Obj, serializerSettings);

            var check = db.Grns.Where(i => i.GrnNumber == grn.GrnNumber && i.Id != grn.Id).FirstOrDefault();
            if (check != null)
            {
                throw new GrnException("GRN already exists.");
            }

            db.SaveChanges();

            var IO = db.InternalOrders.Where(i => i.Id == grn.InternalOrderId).First();

            var test1 = IO.OnceOffItems.Sum(s => s.Quantity);
            var test2 = IO.OnceOffItems.Sum(i => i.GrnonceOffItems.Sum(x => x.Quantity));


            if (IO.OnceOffItems.Sum(s => s.Quantity) == IO.OnceOffItems.Sum(i => i.GrnonceOffItems.Sum(x => x.Quantity))
                && IO.InternalOrderItems.Sum(s => s.Quantity) == IO.InternalOrderItems.Sum(i => i.Grnitems.Sum(x => x.Quantity)))
            {

                IO.StatusId = (int)DAL.Constants.InternalOrderStatus.CLOSE;
                db.SaveChanges();
            }

            return Obj;
        }

        //delete an grn by sending in the grn number
        public static List<string> RemoveGrn(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            List<string> dltBarcode = new List<string>();
            var grnObj = db.Grns.FirstOrDefault(s => s.Id == id);
            if (grnObj == null) throw new GrnException("GRN does not exist.");

            foreach (var item in grnObj.Grnitems)
            {
                var initialstockremove = new DAL.DTO.StockQuantity();
                initialstockremove.StockId = item.InternalOrderItem.StockId;
                for (int i = 0; i < item.Quantity; i++)
                {
                    dltBarcode.Add(""+DAL.StockManage.removeStock(initialstockremove));
                }
            }


            db.Grnitems.RemoveRange(grnObj.Grnitems);
            db.GrnonceOffItems.RemoveRange(grnObj.GrnonceOffItems);

            db.Grns.Remove(grnObj);
            db.SaveChanges();

            return dltBarcode;
        }

        //get current gty or grn
        public static int GetQtyGrn(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var qty = db.Grnitems.FirstOrDefault(a => a.Id == id).Quantity;
            return qty;
        }

        public static object GetLatestGrn(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var check = db.LatestGrns.Where(i => i.InternalOrderId == id).FirstOrDefault();
            if (check == null)
            {
                var latestEntry = new DAL.Models.LatestGrn
                {
                    InternalOrderId = id,
                    GrnIncrement = 0
                };
                db.LatestGrns.Add(latestEntry);
                db.SaveChanges();
            }

            var LatestGrnNr = db.LatestGrns.Where(s => s.InternalOrderId == id).FirstOrDefault().GrnIncrement;

            LatestGrnNr = LatestGrnNr + 1;
            return LatestGrnNr;
        }

    }
}
