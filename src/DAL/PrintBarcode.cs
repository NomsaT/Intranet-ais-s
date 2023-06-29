using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class PrintBarcode
    {
        public static IQueryable<DAL.DTO.StockQuantity> getPrintBarcode()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
                .Where(a => a.Stock.Monitored == true && a.BarcodePrinted == null)
               .Select(p => new DAL.DTO.StockQuantity
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   Code = p.Stock.Code,
                   StockFullName = p.Stock.Code + " - " + p.Stock.InternalProductName + (p.Splitted == true ? " (Split)" : ""),
                   ItemQuantity = p.ItemQuantity,
                   DateCreated = p.DateCreated,
                   DateModified = p.DateModified,
                   Price = p.Price,
                   DepartmentId = p.DepartmentId,
                   LocationId = p.LocationId,
                   StoreId = p.StoreId,
                   uomid = p.Stock.Uomid,
                   UomName = p.Stock.Uom.Name,
                   SupplierCurrencyId = p.Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Stock.Supplier.Currency.Iso,
                   GrnNumber = p.GrnNumber,
                   Barcode = "STQ" + p.Id.ToString()
               }).AsQueryable();
            return source;
        }


        public static IQueryable<DAL.DTO.StockQuantity> getReprintBarcode()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
                .Where(a => a.Stock.Monitored == true && a.BarcodePrinted == true)
               .Select(p => new DAL.DTO.StockQuantity
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   Code = p.Stock.Code,
                   StockFullName = p.Stock.Code + " - " + p.Stock.InternalProductName + (p.Splitted == true ? " (Split)" : ""),
                   ItemQuantity = p.ItemQuantity,
                   DateCreated = p.DateCreated,
                   DateModified = p.DateModified,
                   Price = p.Price,
                   DepartmentId = p.DepartmentId,
                   LocationId = p.LocationId,
                   StoreId = p.StoreId,
                   uomid = p.Stock.Uomid,
                   UomName = p.Stock.Uom.Name,
                   SupplierCurrencyId = p.Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Stock.Supplier.Currency.Iso,
                   GrnNumber = p.GrnNumber,
                   Barcode = "STQ" + p.Id.ToString()
               });
            return source;
        }

        public static IQueryable<DAL.DTO.StockQuantity> getBarcodeVerificationReport()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities
                /*.Where(a => a.Stock.Monitored == true && a.BarcodePrinted == true)*/
                .Where(a => a.VerificationScan != true && a.BarcodePrinted == true)
               .Select(p => new DAL.DTO.StockQuantity
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   Code = p.Stock.Code,
                   StockFullName = p.Stock.Code + " - " + p.Stock.InternalProductName,
                   ItemQuantity = p.ItemQuantity,
                   DateCreated = p.DateCreated,
                   DateModified = p.DateModified,
                   Price = p.Price,
                   DepartmentId = p.DepartmentId,
                   LocationId = p.LocationId,
                   StoreId = p.StoreId,
                   uomid = p.Stock.Uomid,
                   UomName = p.Stock.Uom.Name,
                   SupplierCurrencyId = p.Stock.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Stock.Supplier.Currency.Iso,
                   Barcode = "STQ" + p.Id.ToString(),
                   BarcodePrinted = p.BarcodePrinted,
                   VerificationScan = p.VerificationScan
               });
            return source;
        }

        public static int markPrinted(int barcode)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var item = db.StockQuantities.Where(a => a.Id == barcode).FirstOrDefault();

            if (item == null)
            {
                throw new PrintBarcodeException("Barcode not marked as print.");
            }

            item.BarcodePrinted = true;
            db.SaveChanges();

            return item.Id;
        }
        public static bool verificationScan(List<int> barcodes)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var item = new DAL.Models.StockQuantity();

            foreach (var barcode in barcodes)
            {
                item = db.StockQuantities.Where(a => a.Id == barcode).FirstOrDefault();
                if (item == null)
                {
                    throw new PrintBarcodeException("Item Not found.");
                }
                item.VerificationScan = true;
                db.SaveChangesAsync();
            }
            return true;
        }
    }
}
