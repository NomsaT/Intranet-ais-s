using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Stock
    {
        public static IQueryable<DAL.DTO.Stock> getStock()
        {
            var abbr = "STK";
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   Code = p.Code,
                   ProductName = p.ProductName,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   InternalProductNamePack = p.InternalProductName + " ("+p.PackSize+" Pack)",
                   Uomid = p.Uomid,
                   StockCategoryId = p.StockCategoryId,
                   MinThreshold = p.MinThreshold,
                   //Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                   Quantity = (decimal)p.StockQuantities.Where(i => i.Store.Quarantine == false).Sum(e => e.ItemQuantity),
                   QuarantineQuantity = (decimal)p.StockQuantities.Where(i => i.Store.Quarantine == true).Sum(e => e.ItemQuantity),
                   ProductionQuantity = (decimal)p.StockQuantities.Where(i => i.Store.ProductionStore == true).Sum(e => e.ItemQuantity),
                   SecondQuantity = (p.SecondaryUomid != null) ? (decimal)(p.StockQuantities.Sum(e => e.ItemQuantity) * p.CalculatedRatio) : 0,
                   //Price = p.StockQuantities.Sum(e => (e.Price * e.Stock.PackQuantity)),
                   Price = p.StockQuantities.Where(i => i.Store.ProductionStore == false).Sum(e => e.Price),
                   PriceLookUpPrice = (p.PriceLookups.FirstOrDefault() == null) ? 0 : p.PriceLookups.First().Price,
                   UomName = p.Uom.Name,
                   CategoryName = p.StockCategory.Name,
                   StockGroupId = p.StockGroupId,
                   Monitored = p.Monitored,
                   SupplierId = p.SupplierId,
                   SupplierCurrencyId = p.Supplier.CurrencyId,
                   SupplierCurrencyIso = p.Supplier.Currency.Iso,
                   StoreTypeId = p.DefaultStore.StoreType.Id,
                   DefaultDepartmentId = p.DefaultDepartmentId,
                   DefaultLocationId = p.DefaultLocationId,
                   DefaultStoreId = p.DefaultStoreId,
                   CurrentPrice = p.CurrentPrice,
                   PackSize = p.PackSize,
                   PackQuantity = p.PackQuantity,
                   ItemQuantity = p.ItemQuantity,
                   ItemPrice = p.ItemPrice,
                   SecondaryUomid = p.SecondaryUomid,
                   SecondUomName = p.SecondaryUom.Name,
                   ComparisonSecondValue = p.ComparisonSecondValue,
                   CalculatedRatio = p.CalculatedRatio,
                   DeductedValue = p.DeductedValue,
                   ProductWeight = p.ProductWeight,
                   ProductDimensionsLength = p.ProductDimensionsLength,
                   ProductDimensionsWidth = p.ProductDimensionsWidth,
                   ProductDimensionsHeight = p.ProductDimensionsHeight,
                   ShippingWeight = p.ShippingWeight,
                   ShippingDimensionsLength = p.ShippingDimensionsLength,
                   ShippingDimensionsWidth = p.ShippingDimensionsWidth,
                   ShippingDimensionsHeight = p.ShippingDimensionsHeight,
                   StorageTypeId = p.StorageTypeId,
                   ShelfLifeDays = p.ShelfLifeDays,
                   ShelfLifeTypeId = p.ShelfLifeTypeId,
                   BinId = p.BinId
                   //Barcode = abbr + p.Id.ToString("D9")
               });
            return source;
        }
        //Using this method for a scanner
        public static int GetStockCount(string code, int locId, int storeId)
        {
            //if(code.Substring(0, 3) != "STK")
            //{
            //    throw new System.Exception("Not a stock barcode");
            //}
            using (var db = new DAL.Models.AISContext())
            {
                var stock = db.Stocks.FirstOrDefault(x => x.Id == int.Parse(code));

                if (stock == null)
                {
                    throw new System.Exception("No stock was found");
                }

                return stock.StockQuantities
                        .Where(
                            x => x.StockId == stock.Id &&
                            x.LocationId == locId &&
                            x.StoreId == storeId)
                        .Count();
            }
        }

        public static IQueryable<DAL.DTO.Stock> getStockWithQty()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks.Where(s => s.StockQuantities.Count > 0)
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   Code = p.Code,
                   ProductName = p.ProductName,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   Uomid = p.Uomid,
                   StockCategoryId = p.StockCategoryId,
                   MinThreshold = p.MinThreshold,
                   Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                   SecondQuantity = (p.SecondaryUomid != null) ? (decimal)(p.StockQuantities.Sum(e => e.ItemQuantity) * p.CalculatedRatio) : 0,
                   Price = p.StockQuantities.Sum(e => e.Price),
                   PriceLookUpPrice = (p.PriceLookups.FirstOrDefault() == null) ? 0 : p.PriceLookups.First().Price,
                   UomName = p.Uom.Name,
                   CategoryName = p.StockCategory.Name,
                   StockGroupId = p.StockGroupId,
                   Monitored = p.Monitored,
                   SupplierId = p.SupplierId,
                   SupplierCurrencyId = p.Supplier.CurrencyId,
                   StoreTypeId = p.DefaultStore.StoreType.Id,
                   DefaultDepartmentId = p.DefaultDepartmentId,
                   DefaultLocationId = p.DefaultLocationId,
                   DefaultStoreId = p.DefaultStoreId,
                   CurrentPrice = p.CurrentPrice,
                   PackSize = p.PackSize,
                   PackQuantity = p.PackQuantity,
                   ItemQuantity = p.ItemQuantity,
                   ItemPrice = p.ItemPrice,
                   SecondaryUomid = p.SecondaryUomid,
                   SecondUomName = p.SecondaryUom.Name,
                   ComparisonSecondValue = p.ComparisonSecondValue,
                   CalculatedRatio = p.CalculatedRatio,
                   DeductedValue = p.DeductedValue,
                   ProductWeight = p.ProductWeight,
                   ProductDimensionsLength = p.ProductDimensionsLength,
                   ProductDimensionsWidth = p.ProductDimensionsWidth,
                   ProductDimensionsHeight = p.ProductDimensionsHeight,
                   ShippingWeight = p.ShippingWeight,
                   ShippingDimensionsLength = p.ShippingDimensionsLength,
                   ShippingDimensionsWidth = p.ShippingDimensionsWidth,
                   ShippingDimensionsHeight = p.ShippingDimensionsHeight,
                   StorageTypeId = p.StorageTypeId,
                   ShelfLifeDays = p.ShelfLifeDays,
                   ShelfLifeTypeId = p.ShelfLifeTypeId,
                   BinId = p.BinId
               });
            return source;
        }

        public static DAL.DTO.Stock getStockDataEdit(int stockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            DAL.DTO.Stock data = new DAL.DTO.Stock();

            var p = db.Stocks.Find(stockId);

            data = new DAL.DTO.Stock
            {
                Id = p.Id,
                Code = p.Code,
                ProductName = p.ProductName,
                InternalSku = p.InternalSku,
                InternalProductName = p.InternalProductName,
                InternalProductNameFull = p.Code + " - " + p.InternalProductName,
                Uomid = p.Uomid,
                StockCategoryId = p.StockCategoryId,
                MinThreshold = p.MinThreshold,
                Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                Price = p.StockQuantities.Sum(e => e.Price),
                PriceLookUpPrice = (p.PriceLookups.FirstOrDefault() == null) ? 0 : p.PriceLookups.First().Price,
                UomName = p.Uom.Name,
                CategoryName = p.StockCategory.Name,
                StockGroupId = p.StockGroupId,
                Monitored = p.Monitored,
                SupplierId = p.SupplierId,
                SupplierCurrencyId = p.Supplier.CurrencyId,
                StoreTypeId = p.DefaultStore.StoreType.Id,
                DefaultDepartmentId = p.DefaultDepartmentId,
                DefaultLocationId = p.DefaultLocationId,
                DefaultStoreId = p.DefaultStoreId,
                CurrentPrice = p.CurrentPrice,
                PackSize = p.PackSize,
                PackQuantity = p.PackQuantity,
                ItemQuantity = p.ItemQuantity,
                ItemPrice = p.ItemPrice,
                SecondaryUomid = p.SecondaryUomid,
                ComparisonSecondValue = p.ComparisonSecondValue,
                CalculatedRatio = p.CalculatedRatio,
                DeductedValue = p.DeductedValue,
                ProductWeight = p.ProductWeight,
                ProductDimensionsLength = p.ProductDimensionsLength,
                ProductDimensionsWidth = p.ProductDimensionsWidth,
                ProductDimensionsHeight = p.ProductDimensionsHeight,
                ShippingWeight = p.ShippingWeight,
                ShippingDimensionsLength = p.ShippingDimensionsLength,
                ShippingDimensionsWidth = p.ShippingDimensionsWidth,
                ShippingDimensionsHeight = p.ShippingDimensionsHeight,
                StorageTypeId = p.StorageTypeId,
                ShelfLifeDays = p.ShelfLifeDays,
                ShelfLifeTypeId = p.ShelfLifeTypeId,
                BinId = p.BinId
            };

            return data;
        }

        public static IQueryable<DAL.DTO.Stock> getUnassignedStock(int StockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks.Where(s => s.PriceLookups.Count == 0 || s.Id == StockId)
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   Code = p.Code,
                   ProductName = p.ProductName,
                   InternalSku = p.InternalSku,
                   InternalProductName = p.InternalProductName,
                   InternalProductNameFull = p.Code + " - " + p.InternalProductName,
                   Uomid = p.Uomid,
                   StockCategoryId = p.StockCategoryId,
                   MinThreshold = p.MinThreshold,
                   Quantity = p.StockQuantities.Sum(e => e.ItemQuantity),
                   Price = p.StockQuantities.Sum(e => e.Price),
                   UomName = p.Uom.Name,
                   CategoryName = p.StockCategory.Name,
                   Monitored = p.Monitored,
                   SupplierId = p.SupplierId,
                   SupplierCurrencyId = p.Supplier.CurrencyId,
                   StoreTypeId = p.DefaultStore.StoreType.Id,
                   DefaultDepartmentId = p.DefaultDepartmentId,
                   DefaultLocationId = p.DefaultLocationId,
                   DefaultStoreId = p.DefaultStoreId,
                   CurrentPrice = p.CurrentPrice,
                   PackSize = p.PackSize,
                   PackQuantity = p.PackQuantity,
                   ItemQuantity = p.ItemQuantity,
                   ItemPrice = p.ItemPrice,
                   SecondaryUomid = p.SecondaryUomid,
                   ComparisonSecondValue = p.ComparisonSecondValue,
                   CalculatedRatio = p.CalculatedRatio,
                   DeductedValue = p.DeductedValue,
                   ProductWeight = p.ProductWeight,
                   ProductDimensionsLength = p.ProductDimensionsLength,
                   ProductDimensionsWidth = p.ProductDimensionsWidth,
                   ProductDimensionsHeight = p.ProductDimensionsHeight,
                   ShippingWeight = p.ShippingWeight,
                   ShippingDimensionsLength = p.ShippingDimensionsLength,
                   ShippingDimensionsWidth = p.ShippingDimensionsWidth,
                   ShippingDimensionsHeight = p.ShippingDimensionsHeight,
                   StorageTypeId = p.StorageTypeId,
                   ShelfLifeDays = p.ShelfLifeDays,
                   ShelfLifeTypeId = p.ShelfLifeTypeId,
                   BinId = p.BinId
               });
            return source;
        }

        public static int addStock(DAL.DTO.Stock values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Stock();
            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);
            var check = db.Stocks.Where(m => m.Code == Obj.Code).FirstOrDefault();
            if (check != null)
            {
                throw new StockException("Product Code already exists.");
            }

            Obj.InternalSku = "n/a";
            db.Stocks.Add(Obj);
            db.SaveChanges();

            if (values.InitialQuantity != 0)
            {
                var initialstockadd = new DAL.DTO.StockQuantity();
                initialstockadd.StockId = Obj.Id;
                initialstockadd.ItemQuantity = values.InitialQuantity;
                DAL.StockManage.addStock(initialstockadd);
            }
           
            Obj.InternalSku = Obj.Id.ToString("D6");
            db.SaveChanges();

            //TODO: RECIPE
            if (Obj.StockCategoryId == (int)DAL.Constants.StockCategory.RECIPE)
            {
                Obj.Code = "REC" + Obj.Id.ToString("D4");
                db.SaveChanges();
            }

            return Obj.Id;
        }

        public static int editStock(int key, DAL.DTO.Stock values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = db.Stocks.FirstOrDefault(o => o.Id == key);
            if (Obj == null) throw new StockException("Stock does not exist.");

            var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);
            var check = db.Stocks.Where(m => m.Code == Obj.Code && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new StockException("Product Code already exists.");
            }

            db.SaveChanges();

            //TODO: RECIPE
            if (Obj.StockCategoryId == (int)DAL.Constants.StockCategory.RECIPE)
            {
                Obj.Code = "REC" + Obj.Id.ToString("D4");
                db.SaveChanges();
            }
            return Obj.Id;
        }

        public static async Task<string> deleteStock(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Stocks.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockException("Stock does not exist.");

            if (Obj.StockQuantities.Count > 0)
            {
                throw new StockException("There is still a quantity of items assigned to the stock entry.");
            }           

            if (Obj.ProductStocks.Count > 0)
            {
                throw new StockException("The stock item is linked to a product, remove the item from the product before deleting it.");
            }

            if (Obj.RecipeStockComponents.Count > 0)
            {
                throw new StockException("The stock item is a component of a different stock recipe, remove the record/s before deleting it.");
            }

            if (Obj.RecipeStocks.Count > 0)
            {
                db.Recipes.RemoveRange(Obj.RecipeStocks);
                //throw new StockException("Remove the stock items in the recipe of this record before deleting it.");
            }

            if (Obj.PriceLookups.Count > 0)
            {
                db.PriceLookups.RemoveRange(Obj.PriceLookups);
                //Obj.PriceLookups.Clear();
            }
            db.Stocks.Remove(Obj);
            await db.SaveChangesAsync();

            return "Stock of this product deleted";
        }

        public static decimal getTotalStockDep(int depId,int stockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var totalstock = db.StockQuantities.Where(l => l.DepartmentId == depId && l.StockId == stockId).Sum(s => s.ItemQuantity);

            return totalstock;
        }

        public static decimal getTotalStockLocation(int LocationId,int StockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var totalstock = db.StockQuantities.Where(l => l.LocationId == LocationId && l.StockId == StockId).Sum(s => s.ItemQuantity);

            return totalstock;
        }

        public static decimal getTotalStockStore(int LocationId, int StoreId,int StockId)
        {
            Models.AISContext db = new Models.AISContext();

            var totalstock = db.StockQuantities.Where(l => l.LocationId == LocationId && l.StoreId == StoreId && l.StockId==StockId).Sum(s => s.ItemQuantity);

            return totalstock;
        }
        public static object GetStockByName(string name)
        {
            Models.AISContext db = new Models.AISContext();

            var result = db.StockQuantities
                .Where(x => x.Stock.ProductName.ToLower().Equals(name.ToLower()))
                .AsEnumerable()
                .GroupBy(x => new { x.StoreId , x.LocationId })
                .Select(x => new
                {
                    Id = x.FirstOrDefault().Id,
                    Name = x.FirstOrDefault().Stock.ProductName,
                    Count = x.Count(),
                    StockList = x.Select(x => new { Id = x.Id, ItemQty = x.ItemQuantity, Price = x.Price }).ToList(),
                    Location = x.FirstOrDefault().Location.Name,
                    Store = x.FirstOrDefault().Store.Name
                })
                .ToList();
            return result;
        }
    }
}
