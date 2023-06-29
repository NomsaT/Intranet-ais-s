using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class PriceLookUp
    {
        public static IQueryable<DAL.DTO.PriceLookup> getPriceLookUp()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PriceLookups
               .Select(p => new DAL.DTO.PriceLookup
               {
                   Id = p.Id,
                   Price = p.Price,
                   Date = p.Date,
                   Comment = p.Comment,
                   SupplierId = p.SupplierId,
                   StockId = p.StockId,
                   PriceIncreaseCount = p.PriceIncreases.Count,
                   PriceIncreaseDateCount = p.PriceIncreases.Count(d => d.Date <= DateTime.Now),
                   Discount = p.Supplier.Discount,
                   SupplierCurrencyId = p.Supplier.CurrencyId,
                   Currency = p.Supplier.Currency.Iso
               });

            return source;
        }

        public static int addPriceLookUp(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.PriceLookup();
            JsonConvert.PopulateObject(values, Obj);

            DAL.DTO.PriceLookup priceLookup = new DTO.PriceLookup();
            JsonConvert.PopulateObject(values, priceLookup);

            var check = db.PriceLookups.Where(m => m.StockId == Obj.StockId).FirstOrDefault();
            if (check != null)
            {
                throw new PriceLookUpException("Product already exists.");
            }

            var supplier = db.Suppliers.FirstOrDefault(s => s.Id == Obj.SupplierId);
            var stock = db.Stocks.FirstOrDefault(s => s.Id == Obj.StockId);

            if(stock.CurrentPrice != Obj.Price)
            {
                stock.CurrentPrice = Obj.Price;
                //stock.ItemPrice = stock.CurrentPrice * stock.PackSize;
                stock.ItemPrice = stock.CurrentPrice * stock.PackQuantity;
            }

            var log = new DAL.Models.PriceLookupLog
            {
                Stock = stock.Code + " - " + stock.ProductName,
                OldPrice = 0,
                NewPrice = Obj.Price,
                Date = Obj.Date,
                Comment = Obj.Comment,
                Uom = stock.Uom.Name,
                Application = "not yet set",
                Supplier = supplier.CompanyName,
                //TODO add username
                Username = priceLookup.Username,
                InternalProductName = stock.InternalProductName,
                Currency = supplier.Currency.Iso                
            };

            db.PriceLookups.Add(Obj);
            db.SaveChanges();
            db.PriceLookupLogs.Add(log);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editPriceLookUp(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            decimal OldPrice = db.PriceLookups.FirstOrDefault(p => p.Id == key).Price;
            var Obj = await db.PriceLookups.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PriceLookUpException("Price Lookup does not exist.");

            DAL.DTO.PriceLookup priceLookup = new DTO.PriceLookup();
            JsonConvert.PopulateObject(values, priceLookup);

            JsonConvert.PopulateObject(values, Obj);
            var check = db.PriceLookups.Where(m => m.StockId == Obj.StockId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new PriceLookUpException("Product already exists.");
            }

            var supplier = db.Suppliers.FirstOrDefault(s => s.Id == Obj.SupplierId);
            var stock = db.Stocks.FirstOrDefault(s => s.Id == Obj.StockId);

            if (stock.CurrentPrice != Obj.Price)
            {
                stock.CurrentPrice = Obj.Price;
                //stock.ItemPrice = stock.CurrentPrice * stock.PackSize;
                stock.ItemPrice = stock.CurrentPrice * stock.PackQuantity;
            }

            var log = new DAL.Models.PriceLookupLog
            {
                Stock = stock.Code + " - " + stock.ProductName,
                OldPrice = OldPrice,
                NewPrice = Obj.Price,
                Date = Obj.Date,
                Comment = "Price edited via lookup page.",
                Uom = stock.Uom.Name,
                Application = "not yet set",
                Supplier = supplier.CompanyName,
                //TODO add username
                Username = priceLookup.Username,
                InternalProductName = stock.InternalProductName,
                Currency = supplier.Currency.Iso
            };

            db.PriceLookupLogs.Add(log);
            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deletePriceLookUp(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            decimal OldPrice = db.PriceLookups.FirstOrDefault(p => p.Id == key).Price;
            var Obj = await db.PriceLookups.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PriceLookUpException("Price Lookup does not exist.");

            DAL.DTO.PriceLookup priceLookup = new DTO.PriceLookup();
            JsonConvert.PopulateObject(values, priceLookup);

            if (Obj.PriceIncreases.Count > 0)
            {
                throw new PriceLookUpException("Remove the Price Increase Reminder before deleting the current price.");
            }

            var supplier = db.Suppliers.FirstOrDefault(s => s.Id == Obj.SupplierId);
            var stock = db.Stocks.FirstOrDefault(s => s.Id == Obj.StockId);

            var log = new DAL.Models.PriceLookupLog
            {
                Stock = stock.Code + " - " + stock.ProductName,
                OldPrice = OldPrice,
                NewPrice = Obj.Price,
                Date = Obj.Date,
                Comment = "Price deleted via lookup page.",
                Uom = stock.Uom.Name,
                Application = "not yet set",
                Supplier = supplier.CompanyName,
                //TODO add username
                Username = priceLookup.Username,
                InternalProductName = stock.InternalProductName,
                Currency = supplier.Currency.Iso
            };

            db.PriceLookupLogs.Add(log);
            db.PriceLookups.Remove(Obj);
            await db.SaveChangesAsync();

            return "Price Lookup Deleted";
        }

        public static int editManualUpdate(DAL.DTO.PriceIncrease increase)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            decimal OldPrice = db.PriceLookups.FirstOrDefault(p => p.Id == increase.PriceLookUpId).Price;

            var LookUpObj = db.PriceLookups.Where(w => w.Id == increase.PriceLookUpId).FirstOrDefault();
            var IncreaseObj = db.PriceIncreases.Where(m => m.PriceLookUpId == increase.PriceLookUpId).FirstOrDefault();

            if (IncreaseObj == null)
            {
                throw new PriceLookUpException("No increase found.");
            }

            LookUpObj.Date = DateTime.Now;
            LookUpObj.Comment = "Price edited via lookup page.";
            if (increase.IncreaseTypeId == (int)DAL.Constants.IncreaseTypes.CURRENCY)
            {
                LookUpObj.Price = LookUpObj.Price + increase.Increase;
            }
            else if (increase.IncreaseTypeId == (int)DAL.Constants.IncreaseTypes.PERC)
            {
                decimal amount = (increase.Increase / 100) * LookUpObj.Price;
                LookUpObj.Price = LookUpObj.Price + amount;
            }
            else if (increase.IncreaseTypeId == (int)DAL.Constants.IncreaseTypes.NEWPRICE)
            {
                LookUpObj.Price = increase.Increase;
            }

            var supplier = db.Suppliers.FirstOrDefault(s => s.Id == LookUpObj.SupplierId);
            var stock = db.Stocks.FirstOrDefault(s => s.Id == LookUpObj.StockId);

            stock.CurrentPrice = LookUpObj.Price;
            //stock.ItemPrice = stock.CurrentPrice / stock.PackSize;
            stock.ItemPrice = stock.CurrentPrice * stock.PackQuantity;

            var log = new DAL.Models.PriceLookupLog
            {
                Stock = stock.Code + " - " + stock.ProductName,
                OldPrice = OldPrice,
                NewPrice = LookUpObj.Price,
                Date = DateTime.Now,
                Comment = "Price increase added via lookup page.",
                Uom = stock.Uom.Name,
                Application = "not yet set",
                Supplier = supplier.CompanyName,
                //TODO add username
                Username = increase.Username,
                InternalProductName = stock.InternalProductName,
                Currency = supplier.Currency.Iso
            };

           /* db.Stocks.Add(stock);
            db.SaveChanges();*/

            db.PriceLookupLogs.Add(log);
            db.SaveChanges();

            var Obj = db.PriceIncreases.FirstOrDefault(o => o.PriceLookUpId == LookUpObj.Id);
            if (Obj == null) throw new PriceLookUpException("Price Lookup does not exist.");

            db.PriceIncreases.Remove(Obj);
            db.SaveChanges();

            return LookUpObj.Id;
        }

        public static async void checkMonitoredStatus()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var IO = db.InternalOrders.Where(i => i.StatusId == (int)DAL.Constants.InternalOrderStatus.PENDINGMONITOREDAPPROVAL)
                .Where(i => i.InternalOrderItems.All(r => r.Value == r.Stock.PriceLookups.First().Price));

            foreach (var i in IO)
            {
                i.StatusId = (int)DAL.Constants.InternalOrderStatus.PENDING;
            }

            db.SaveChanges();
        }
    }
}
