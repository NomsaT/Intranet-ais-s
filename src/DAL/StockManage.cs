using DAL.Models;
using Microsoft.EntityFrameworkCore;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;

namespace DAL
{
    public static class StockManage
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public static bool checkRecipeQuantity(DAL.DTO.StockQuantity stock)
        {
            bool recipe = false;
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stock.StockId);
            /*TODO: RECIPE*/
            if (mainStock.StockCategoryId == (int)DAL.Constants.StockCategory.RECIPE)
            {
                recipe = true;
                foreach (var recipeStock in mainStock.RecipeStocks)
                {
                    var currentQuantities = db.StockQuantities.Where(s => s.StockId == recipeStock.StockComponentId && s.DepartmentId == stock.DepartmentId).Sum(r => r.ItemQuantity);
                    var useQuantities = recipeStock.Quantity * stock.ItemQuantity;// * stock.Amount;
                    if (useQuantities > currentQuantities)
                    {
                        throw new StockManageException(string.Format("Not enough stock of {0}, have {1} need {2}", recipeStock.StockComponent.InternalProductName, currentQuantities, useQuantities));
                    }
                }
            }
            return recipe;
        }

        public static void removeRecipeStock(int stockId, decimal quantity, int departmentId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stockId);

            foreach (var recipeStock in mainStock.RecipeStocks)
            {
                var useQuantities = recipeStock.Quantity * quantity;
                removeStock(new DAL.DTO.StockQuantity { StockId = recipeStock.StockComponentId, ItemQuantity = useQuantities, DepartmentId = departmentId });
            }
        }

        public static int addStock(DAL.DTO.StockQuantity stock)
        {
            //Naming Info
            //PackSize - how many little boxes in big box
            //ItemQuantity - what is the capacity of the little box (2L, 1KG)
            //PackQuantity - PackSize x ItemQuantity (QUantity of big box)    

            DAL.Models.AISContext db = new DAL.Models.AISContext();

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {                   
                    var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stock.StockId);

                    stock.PackSize = mainStock.PackSize;
                    stock.DepartmentId = mainStock.DefaultDepartmentId;
                    stock.LocationId = mainStock.DefaultLocationId;
                    stock.StoreId = mainStock.DefaultStoreId;
                    //stock.Quantity = mainStock.PackQuantity;
                    stock.Price = mainStock.CurrentPrice;
                    bool recipe = checkRecipeQuantity(stock);
                    if (recipe)
                    {
                        removeRecipeStock(stock.StockId, stock.ItemQuantity, stock.DepartmentId);
                    }

                    string command = "EXECUTE AddStock @StockId = " + stock.StockId + ",@ItemQuantity=" + mainStock.ItemQuantity.ToString(CultureInfo.InvariantCulture) + ",@Price=" + mainStock.ItemPrice.ToString(CultureInfo.InvariantCulture) + ",@DepartmentId=" + stock.DepartmentId + ",@LocationId=" + stock.LocationId + ",@StoreId=" + stock.StoreId + ",@Packsize=" + stock.PackSize + ",@InsertedQuantity=" + stock.ItemQuantity + ",@GrnNumber='" + stock.GrnNumber + "'";
                    logger.Trace(command);

                    try
                    {
                        db.Database.ExecuteSqlRaw(command);

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex);
                        throw ex;
                    }
                    

                    var StockObj = db.Stocks.Find(stock.StockId);
                    var DepartmentObj = db.Departments.Find(stock.DepartmentId);
                    var StoreObj = db.Stores.Find(stock.StoreId);

                    var qty = (stock.PackSize * mainStock.ItemQuantity) * stock.ItemQuantity;
                    var totalpriceqty = (stock.PackSize * stock.ItemQuantity) * StockObj.ItemPrice;

                    var locationName = "N/A";
                    if (stock.LocationId != null)
                    {
                        var LocationObj = db.PlantLocations.Find(stock.LocationId);
                        locationName = LocationObj.Name;
                    }
                    var log = new DAL.Models.StockLog
                    {
                        ProductCodeName = StockObj.Code + " " + StockObj.InternalProductName,
                        Date = DateTime.Now,
                        Quantity = qty,//mainStock.ItemQuantity,
                        Price = totalpriceqty,//StockObj.ItemPrice,
                        Department = DepartmentObj.Name,
                        Action = "Stock Quantity Added",
                        Location = locationName,
                        Store = StoreObj.Name,
                        StockCategory = StockObj.StockCategory.Name,
                        StockGroup = (StockObj.StockGroup == null) ? null : StockObj.StockGroup.Name,
                        Uom = StockObj.Uom.Name,
                        Supplier = StockObj.Supplier.CompanyName,
                        InternalProductName = StockObj.InternalProductName,
                        SupplierCurrency = StockObj.Supplier.Currency.Iso
                    };
                    db.StockLogs.Add(log);
                    db.SaveChanges();

                    dbContextTransaction.Commit();
                    return 1;

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw ex;
                }
            }
        }

        public static int removeStock(DAL.DTO.StockQuantity stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stock.StockId);

            if (stock.uomid != mainStock.Uomid && stock.uomid != 0)
            {

                stock.ItemQuantity = stock.ItemQuantity / (decimal)mainStock.CalculatedRatio;
            }

            if (StockQuantity < stock.ItemQuantity)
            {
                throw new StockException("Quantity in store less than " + stock.ItemQuantity);
            }

            var quantity = stock.ItemQuantity;
            var StockObjList = db.StockQuantities.Where(o => o.StockId == stock.StockId && o.ItemQuantity > 0).OrderBy(d => d.DateCreated);

            foreach (var StockObj in StockObjList)
            {
                if (StockObj.ItemQuantity <= quantity)
                {
                    quantity -= StockObj.ItemQuantity;
                    //calculate the stockObj Price
                    var log = new DAL.Models.StockLog
                    {
                        ProductCodeName = StockObj.Stock.Code + " " + StockObj.Stock.InternalProductName,
                        Date = DateTime.Now,
                        Quantity = StockObj.ItemQuantity,
                        Price = StockObj.Price,
                        Department = StockObj.Department.Name,
                        Action = "Stock Quantity Removed",
                        Location = StockObj.Location?.Name,
                        Store = StockObj.Store?.Name,
                        StockCategory = StockObj.Stock.StockCategory.Name,
                        StockGroup = (StockObj.Stock.StockGroup == null) ? null : StockObj.Stock.StockGroup.Name,
                        Uom = StockObj.Stock.Uom.Name,
                        Supplier = StockObj.Stock.Supplier.CompanyName,
                        InternalProductName = StockObj.Stock.InternalProductName,
                        SupplierCurrency = StockObj.Stock.Supplier.Currency.Iso
                    };
                    db.StockLogs.Add(log);
                    StockObj.ItemQuantity = 0;
                    db.StockQuantities.Remove(StockObj);
                }
                else
                {

                    var per = 1 - (quantity/StockObj.ItemQuantity);
                    var newprice = StockObj.Price * per;

                    StockObj.ItemQuantity -= quantity;
                    StockObj.DateModified = DateTime.Now;
                    StockObj.Price = newprice;

                    var log = new DAL.Models.StockLog
                    {
                        ProductCodeName = StockObj.Stock.Code + " " + StockObj.Stock.InternalProductName,
                        Date = DateTime.Now,
                        Quantity = quantity,
                        Price = StockObj.Price,
                        Department = StockObj.Department.Name,
                        Action = "Stock Quantity Removed",
                        Location = StockObj.Location?.Name,
                        Store = StockObj.Store?.Name,
                        StockCategory = StockObj.Stock.StockCategory.Name,
                        StockGroup = (StockObj.Stock.StockGroup == null) ? null : StockObj.Stock.StockGroup.Name,
                        Uom = StockObj.Stock.Uom.Name,
                        Supplier = StockObj.Stock.Supplier.CompanyName,
                        InternalProductName = StockObj.Stock.InternalProductName,
                        SupplierCurrency = StockObj.Stock.Supplier.Currency.Iso
                    };
                    db.StockLogs.Add(log);
                    quantity = 0;
                    break;
                }
            }

            db.SaveChanges();
            return stock.StockId;
        }

        public static int removeConsumeStock(DAL.DTO.StockQuantity stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockId = db.StockQuantities.FirstOrDefault(i => i.Id == stock.StockId).StockId;
            var StockQuantity = db.StockQuantities.Where(o => o.StockId == stockId).Select(q => q.ItemQuantity).Sum();

            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stockId);

            if (stock.uomid != mainStock.Uomid && stock.uomid != 0)
            {

                stock.ItemQuantity = stock.ItemQuantity / (decimal)mainStock.CalculatedRatio;
            }

            if (StockQuantity < stock.ItemQuantity)
            {
                throw new StockException("Quantity in store less than " + stock.ItemQuantity);
            }

            var quantity = stock.ItemQuantity;
            var StockObjList = db.StockQuantities.Where(o => o.StockId == stockId && o.ItemQuantity > 0).OrderBy(d => d.DateCreated);

            foreach (var StockObj in StockObjList)
            {
                if (StockObj.ItemQuantity <= quantity)
                {
                    quantity -= StockObj.ItemQuantity;
                    //calculate the stockObj Price
                    var log = new DAL.Models.StockLog
                    {
                        ProductCodeName = StockObj.Stock.Code + " " + StockObj.Stock.InternalProductName,
                        Date = DateTime.Now,
                        Quantity = StockObj.ItemQuantity,
                        Price = StockObj.Price,
                        Department = StockObj.Department.Name,
                        Action = "Stock Quantity Removed",
                        Location = StockObj.Location?.Name,
                        Store = StockObj.Store?.Name,
                        StockCategory = StockObj.Stock.StockCategory.Name,
                        StockGroup = (StockObj.Stock.StockGroup == null) ? null : StockObj.Stock.StockGroup.Name,
                        Uom = StockObj.Stock.Uom.Name,
                        Supplier = StockObj.Stock.Supplier.CompanyName,
                        InternalProductName = StockObj.Stock.InternalProductName,
                        SupplierCurrency = StockObj.Stock.Supplier.Currency.Iso
                    };
                    db.StockLogs.Add(log);
                    StockObj.ItemQuantity = 0;
                    db.StockQuantities.Remove(StockObj);
                }
                else
                {

                    var per = 1 - (quantity / StockObj.ItemQuantity);
                    var newprice = StockObj.Price * per;

                    StockObj.ItemQuantity -= quantity;
                    StockObj.DateModified = DateTime.Now;
                    StockObj.Price = newprice;

                    var log = new DAL.Models.StockLog
                    {
                        ProductCodeName = StockObj.Stock.Code + " " + StockObj.Stock.InternalProductName,
                        Date = DateTime.Now,
                        Quantity = quantity,
                        Price = StockObj.Price,
                        Department = StockObj.Department.Name,
                        Action = "Stock Quantity Removed",
                        Location = StockObj.Location?.Name,
                        Store = StockObj.Store?.Name,
                        StockCategory = StockObj.Stock.StockCategory.Name,
                        StockGroup = (StockObj.Stock.StockGroup == null) ? null : StockObj.Stock.StockGroup.Name,
                        Uom = StockObj.Stock.Uom.Name,
                        Supplier = StockObj.Stock.Supplier.CompanyName,
                        InternalProductName = StockObj.Stock.InternalProductName,
                        SupplierCurrency = StockObj.Stock.Supplier.Currency.Iso
                    };
                    db.StockLogs.Add(log);
                    quantity = 0;
                    break;
                }
            }

            db.SaveChanges();
            return stockId;
        }

        public static string removeStockItem(DAL.DTO.StockQuantity stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var barcode = "";
            StockQuantity stockItem = db.StockQuantities.FirstOrDefault(s => s.StockId == stock.Id);
            if (stockItem != null)
            {
                //Update the stock item if the qty is less than pack move qty
                //minus the qty that you consume then
                if (stock.DepartmentId != 0)
                {
                    //stockItem.DepartmentId = stock.DepartmentId;
                    consumeStock(stockItem, stock);
                }
                else
                {
                    //stockItem.LocationId = stock.LocationId;
                    //stockItem.StoreId = stock.StoreId;
                    consumeStock(stockItem, stock);
                }


            }
            else
            { 
                throw new Exception("No stock Item was found");
            }
            return "Successfully consumed stock item";

        }

        public static void consumeStock(StockQuantity stockItem, DAL.DTO.StockQuantity stock)
        {
            AISContext db = new AISContext();
            var orginalPrice = stockItem.Price;
            string barcode = "";
            var pl = db.PlantLocations.AsNoTracking().FirstOrDefault(p => p.Id == stock.LocationId);
            var store = db.Stores.AsNoTracking().FirstOrDefault(s => s.Id == stock.StoreId);
            var dep = db.Departments.AsNoTracking().FirstOrDefault(d => d.Id == stock.DepartmentId);
            var newStock = db.Stocks.AsNoTracking().FirstOrDefault(s => s.Id == stock.StockId);

            if (dep == null)
            {
                dep = db.Departments.AsNoTracking().FirstOrDefault(x => x.Id == newStock.DefaultDepartmentId);
            }

            if (stock.ItemQuantity != 0 && stock.ItemQuantity < stockItem.ItemQuantity)
            {
                barcode = "STQ" + stockItem.Id;
                var per = 1 - (stock.ItemQuantity/stockItem.ItemQuantity);
                stockItem.ItemQuantity = stockItem.ItemQuantity - stock.ItemQuantity;

                var newprice = stockItem.Price * per;
                stockItem.Price = newprice;
                stockItem.DateModified = DateTime.Now;
                var updatedStockItem = db.StockQuantities.Update(stockItem).Entity;
                db.SaveChanges();
                DbLogging(updatedStockItem, "Updated Stock Item");
                db.SaveChanges();

                //The new Item after consumption
                var newItem = new StockQuantity
                {
                    StoreId = store.Id,
                    LocationId = pl.Id,
                    //Location = pl,
                    //Store= store,
                    DepartmentId = dep.Id,
                    //Department = dep,
                    StockId = stock.StockId,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    ItemQuantity = stock.ItemQuantity,
                    Price = orginalPrice - stockItem.Price,
                    //Stock = newStock
                };
                var newAdded = db.StockQuantities.Add(newItem).Entity;
                //newAdded.Department = dep;
                db.SaveChanges();
                DbLogging(newItem, "Added New Stock Item");
                barcode = "STQ" + stockItem.Id;
            }
            else if (stock.ItemQuantity != 0 && stock.ItemQuantity == stockItem.ItemQuantity)
            {

                barcode = "STQ" + stockItem.Id;
                //var per = 1 - (stock.ItemQuantity/stockItem.ItemQuantity);
                stockItem.ItemQuantity = 0;
                //var newprice = stockItem.Price * per;
                stockItem.Price = 0;
                stockItem.DateModified = DateTime.Now;
                var updatedStockItem = db.StockQuantities.Update(stockItem).Entity;
                db.SaveChanges();
                DbLogging(updatedStockItem, "Updated Stock Item");


                //The new Item after consumption
                var newItem = new StockQuantity
                {
                    StoreId = store.Id,
                    LocationId = pl.Id,
                    //Location = pl,
                    //Store= store,
                    DepartmentId = dep.Id,
                    //Department = dep,
                    StockId = stock.StockId,
                    DateCreated = DateTime.Now,
                    DateModified = DateTime.Now,
                    ItemQuantity = stock.ItemQuantity,
                    Price = orginalPrice - stockItem.Price,
                    //Stock = newStock
                };
                var newAdded = db.StockQuantities.Add(newItem).Entity;
                //newAdded.Department = dep;
                db.SaveChanges();
                DbLogging(newItem, "Added New Stock Item");
                barcode = "STQ" + stockItem.Id;
            }
            else
            {
                throw new InvalidExpressionException("You are trying to consume more than you have");
            }
        }

        public static async void DbLogging(StockQuantity stockQuantity, string action)
        {
            AISContext context = new AISContext();

            if (stockQuantity.Department == null)
            {
                stockQuantity.Department = context.Departments.FirstOrDefault(x => x.Id == stockQuantity.DepartmentId);
                stockQuantity.Location = context.PlantLocations.FirstOrDefault(x => x.Id == stockQuantity.LocationId);
                stockQuantity.Store = context.Stores.FirstOrDefault(x => x.Id == stockQuantity.StoreId);
            }
            var log = new DAL.Models.StockLog
            {
                ProductCodeName = stockQuantity.Stock.Code + " " + stockQuantity.Stock.InternalProductName,
                Date = DateTime.Now,
                Quantity = stockQuantity.ItemQuantity,
                Price = stockQuantity.Price,
                Department = stockQuantity.Department.Name,
                Action = action,
                Location = stockQuantity.Location?.Name,
                Store = stockQuantity.Store?.Name,
                StockCategory = stockQuantity.Stock.StockCategory.Name,
                StockGroup = (stockQuantity.Stock.StockGroup == null) ? null : stockQuantity.Stock.StockGroup.Name,
                Uom = stockQuantity.Stock.Uom.Name,
                Supplier = stockQuantity.Stock.Supplier.CompanyName,
                InternalProductName = stockQuantity.Stock.InternalProductName,
                SupplierCurrency = stockQuantity.Stock.Supplier.Currency.Iso
            };
            context.StockLogs.Add(log);
            await context.SaveChangesAsync();
        }

        //TODO: Remove set stock from system
        /* public static int setStock(DAL.DTO.StockQuantity stock)
         {
             stock.PackSize = 1;
             DAL.Models.AISContext db = new DAL.Models.AISContext();
             var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

             if (StockQuantity == stock.ItemQuantity)
             {
                 throw new StockException("Stock in store is already " + stock.ItemQuantity);
             }

             if (StockQuantity > stock.ItemQuantity)
             {
                 stock.ItemQuantity = StockQuantity - stock.ItemQuantity;
                 return removeStock(stock);
             }
             else
             {
                 stock.ItemQuantity -= StockQuantity;
                 return addStock(stock);
             }
         }*/

        public static object getStockPrice(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.PriceLookups.Where(s => s.StockId == id)
               .Select(i => new DAL.DTO.PriceLookup
               {
                   Id = i.Id,
                   Price = i.Price,
                   Date = i.Date,
                   Comment = i.Comment,
                   SupplierId = i.SupplierId,
                   StockId = i.StockId
               });
            return source;
        }

        public static object getStockUOM(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.UnitOfMeasurements.Where(s => s.Id == id)
               .Select(i => new DAL.DTO.UnitOfMeasurement
               {
                   Id = i.Id,
                   Name = i.Name
               });
            return source;
        }

        public static object getMinThreshold(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.StockGroups.Where(s => s.Id == id).FirstOrDefault()?.MinThreshold;
            return source;
        }

        public static bool CheckSKU(string code, int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            bool valid = false;

            if (id > 0)
            {
                var source = db.Stocks.Where(s => s.Code == code && s.Id != id).FirstOrDefault();
                if (source == null)
                {
                    valid = true;
                }
            }
            else
            {
                var source = db.Stocks.Where(s => s.Code == code).FirstOrDefault();
                if (source == null)
                {
                    valid = true;
                }
            }

            return valid;
        }

        public static DAL.DTO.StockRecipe getMixRecipe(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            DAL.DTO.StockRecipe stock = new DAL.DTO.StockRecipe();

            stock.StockId = id;
            stock.Recipe = db.Recipes.Where(s => s.StockId == id)
               .Select(i => new DAL.DTO.Recipe
               {
                   Id = i.Id,
                   StockId = i.StockId,
                   StockComponentId = i.StockComponentId,
                   Quantity = i.Quantity,
                   UomName = i.StockComponent.Uom.Name
               }).ToList();
            return stock;
        }

        public static int mixStock(DAL.DTO.StockRecipe stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var dltObj = db.Recipes.Where(r => r.StockId == stock.StockId);
            db.RemoveRange(dltObj);
            foreach (var item in stock.Recipe)
            {
                db.Recipes.Add(new Models.Recipe { StockId = stock.StockId, StockComponentId = item.StockComponentId, Quantity = item.Quantity });
            }
            db.SaveChanges();

            return stock.StockId;
        }

        public static int transferStock(DAL.DTO.TransferStockDepartment stock)
        {
            //transfer by department
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockItem = db.Stocks.Where(i => i.Id == stock.StockId).FirstOrDefault();

            switch (stock.quantityOption)
            {
                case "UoM":
                    {
                        var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

                        if (stock.uomid != stockItem.Uomid)
                        {

                            stock.PackQuantityMove = stock.PackQuantityMove / (decimal)stockItem.CalculatedRatio;
                        }

                        if (StockQuantity < stock.PackQuantityMove)
                        {
                            throw new StockException("Quantity in store less than " + stock.PackQuantityMove);
                        }

                        decimal quantity = stock.PackQuantityMove;
                        var StockObjList = db.StockQuantities.Where(o => o.StockId == stock.StockId && o.DepartmentId == stock.OriginDepartment && o.ItemQuantity > 0).OrderBy(d => d.DateCreated);

                        foreach (var StockObj in StockObjList)
                        {
                            //transfer entire unit qty (entire box milk)
                            if (StockObj.ItemQuantity <= quantity)
                            {
                                quantity -= StockObj.ItemQuantity;
                                StockObj.DepartmentId = stock.NewDepartment;
                            }
                            else
                            {
                                //transfer smaller than original unit qty (qty of box milk)
                                if (quantity > 0)
                                {
                                    var per = (quantity / StockObj.ItemQuantity);
                                    var newprice = StockObj.Price * per;

                                    db.StockQuantities.Add(new Models.StockQuantity
                                    {
                                        ItemQuantity = quantity,
                                        DepartmentId = stock.NewDepartment,
                                        Price = newprice,//StockObj.Price,
                                        StockId = StockObj.StockId,
                                        DateCreated = StockObj.DateCreated,
                                        DateModified = DateTime.Now,
                                        LocationId = StockObj.LocationId,
                                        StoreId = StockObj.StoreId
                                    });
                                    StockObj.ItemQuantity -= quantity;

                                    var perC = 1 - per;
                                    var newpriceC = StockObj.Price * perC;

                                    StockObj.Price = newpriceC;
                                    StockObj.DateModified = DateTime.Now;
                                    quantity = 0;
                                }
                                break;
                            }
                        }
                        db.SaveChanges();
                        break;
                    }
                case "SKU":
                    {

                        var itemsremove = stockItem.PackSize * (int)stock.PackQuantityMove;
                        var list = db.StockQuantities.Where(s => s.StockId == stock.StockId && s.DepartmentId == stock.OriginDepartment && s.ItemQuantity == stockItem.ItemQuantity).OrderBy(d => d.DateCreated).Take(itemsremove);

                        if (itemsremove != list.Count())
                        {
                            throw new StockException("The Department does not have enough stock to transfer.");
                        }

                        list.ToList().ForEach(d => d.DepartmentId = stock.NewDepartment);
                        db.SaveChanges();
                        break;
                    }
            }

            return 1;
        }

        public static int transferStockLocation(DAL.DTO.TransferStockLocation stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockItem = db.Stocks.Where(i => i.Id == stock.StockId).FirstOrDefault();

            switch (stock.quantityOption)
            {
                case "UoM":
                    {
                        var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

                        if (stock.uomid != stockItem.Uomid)
                        {

                            stock.PackQuantityMove = stock.PackQuantityMove / (decimal)stockItem.CalculatedRatio;
                        }

                        if (StockQuantity < stock.PackQuantityMove)
                        {
                            throw new StockException("Quantity in store less than " + stock.PackQuantityMove);
                        }

                        decimal quantity = stock.PackQuantityMove;
                        var StockObjList = db.StockQuantities.Where(o => o.StockId == stock.StockId && o.LocationId == stock.originLocation && o.StoreId == stock.originStore && o.ItemQuantity > 0).OrderBy(d => d.DateCreated);

                        foreach (var StockObj in StockObjList)
                        {
                            if (StockObj.ItemQuantity <= quantity)
                            {
                                quantity -= StockObj.ItemQuantity;
                                StockObj.LocationId = stock.newLocation;
                                StockObj.StoreId = stock.newStore;
                            }
                            else
                            {
                                if (quantity > 0)
                                {
                                    var per = (quantity / StockObj.ItemQuantity);
                                    var newprice = StockObj.Price * per;

                                    db.StockQuantities.Add(new Models.StockQuantity
                                    {
                                        ItemQuantity = quantity,
                                        DepartmentId = StockObj.DepartmentId,
                                        Price = newprice,//StockObj.Price,
                                        StockId = StockObj.StockId,
                                        DateCreated = StockObj.DateCreated,
                                        DateModified = DateTime.Now,
                                        LocationId = stock.newLocation,
                                        StoreId = stock.newStore
                                    });
                                    StockObj.ItemQuantity -= quantity;

                                    var perC = 1 - per;
                                    var newpriceC = StockObj.Price * perC;

                                    StockObj.Price = newpriceC;

                                    StockObj.DateModified = DateTime.Now;
                                    quantity = 0;
                                }
                                break;
                            }
                        }
                        db.SaveChanges();
                        //TODO add stock log
                        break;
                    }
                case "SKU":
                    {
                        var itemsremove = stockItem.PackSize * (int)stock.PackQuantityMove;
                        var list = db.StockQuantities.Where(s => s.StockId == stock.StockId && s.LocationId == stock.originLocation && s.StoreId == stock.originStore && s.ItemQuantity == stockItem.ItemQuantity).OrderBy(d => d.DateCreated).Take(itemsremove);

                        if (itemsremove != list.Count())
                        {
                            throw new StockException("The Location does not have enough stock to transfer.");
                        }

                        list.ToList().ForEach(d => d.LocationId = stock.newLocation);
                        list.ToList().ForEach(d => d.StoreId = stock.newStore);
                        db.SaveChanges();
                        break;
                    }
            }
            return 1;
        }

        //Scanner
        public static int scannerTransferByDepartment(DAL.DTO.TransferStockDepartment stock)
        {
            //transfer by department
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockItem = db.Stocks.Where(i => i.Id == stock.StockId).FirstOrDefault();

            switch (stock.quantityOption)
            {
                case "UoM":
                    {
                        var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

                        if (stock.uomid != stockItem.Uomid)
                        {

                            stock.PackQuantityMove = stock.PackQuantityMove / (decimal)stockItem.CalculatedRatio;
                        }

                        if (StockQuantity < stock.PackQuantityMove)
                        {
                            throw new StockException("Quantity in store less than " + stock.PackQuantityMove);
                        }

                        decimal quantity = stock.PackQuantityMove;
                        var StockObj = db.StockQuantities.FirstOrDefault(x => x.Id.Equals(stock.Id));

                        //foreach (var StockObj in StockObjList)
                        //{
                        //transfer entire unit qty (entire box milk)
                        if (StockObj.ItemQuantity <= quantity)
                        {
                            quantity -= StockObj.ItemQuantity;
                            StockObj.DepartmentId = stock.NewDepartment;
                            db.SaveChanges();
                        }
                        else
                        {
                            //transfer smaller than original unit qty (qty of box milk)
                            if (quantity > 0)
                            {
                                var per = (quantity / StockObj.ItemQuantity);
                                var newprice = StockObj.Price * per;

                                var splitteItem = db.StockQuantities.Add(new Models.StockQuantity
                                {
                                    ItemQuantity = quantity,
                                    DepartmentId = stock.NewDepartment,
                                    Price = newprice,//StockObj.Price,
                                    StockId = StockObj.StockId,
                                    DateCreated = DateTime.Now,
                                    DateModified = DateTime.Now,
                                    LocationId = StockObj.LocationId,
                                    StoreId = StockObj.StoreId,
                                    BarcodePrinted = null,
                                    Splitted = true
                                }).Entity;
                                DbLogging(splitteItem, "Moved/Added Stock Item By UOM");
                                StockObj.ItemQuantity -= quantity;

                                var perC = 1 - per;
                                var newpriceC = StockObj.Price * perC;

                                StockObj.Price = newpriceC;
                                StockObj.DateModified = DateTime.Now;
                                quantity = 0;
                                db.SaveChanges();
                                DbLogging(StockObj, "Updated Stock Item After Being Consumed");
                            }
                            break;
                        }
                        //}

                        break;
                    }
                case "SKU":
                    {

                        var itemsremove = stockItem.PackSize * (int)stock.PackQuantityMove;
                        var list = db.StockQuantities.Where(s => s.StockId == stock.StockId && s.DepartmentId == stock.OriginDepartment && s.ItemQuantity == stockItem.ItemQuantity).OrderBy(d => d.DateCreated).Take(itemsremove);

                        if (itemsremove != list.Count())
                        {
                            throw new StockException("The Department does not have enough stock to transfer.");
                        }

                        list.ToList().ForEach(d => d.DepartmentId = stock.NewDepartment);
                        list.ToList().ForEach(d => d.DateModified = DateTime.Now);
                        db.SaveChanges();
                        DbLogging(list.FirstOrDefault(), "Moved Stock Item By SKU");
                        break;
                    }
                case "C_UoM":
                    {
                        var stockQty = db.StockQuantities
                             .Select(x => new DTO.StockQuantity
                             {
                                 Id= x.Id,
                                 uomid = stock.uomid,
                                 ItemQuantity = stock.PackQuantityMove,
                                 StockId = x.Stock.Id,
                                 LocationId = x.LocationId,
                                 StoreId = x.StoreId,
                             }).FirstOrDefault(x => x.StockId == stock.StockId && x.Id == stock.Id);

                        stockQty.DepartmentId = stock.NewDepartment;

                        string removed = removeStockItem(stockQty);
                        break;
                    }
            }

            return 1;
        }

        public static string scannerTransferByLocation(DAL.DTO.TransferStockLocation stock)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockItem = db.Stocks.Where(i => i.Id == stock.StockId).FirstOrDefault();
            string results = "";

            switch (stock.quantityOption)
            {
                case "UoM":
                    {
                        var StockQuantity = db.StockQuantities.Where(o => o.StockId == stock.StockId).Select(q => q.ItemQuantity).Sum();

                        if (stock.uomid != stockItem.Uomid)
                        {
                            stock.PackQuantityMove = stock.PackQuantityMove / (decimal)stockItem.CalculatedRatio;
                        }

                        if (StockQuantity < stock.PackQuantityMove)
                        {
                            throw new StockException("Quantity in store less than " + stock.PackQuantityMove);
                        }

                        decimal quantity = stock.PackQuantityMove;
                        var StockObj = db.StockQuantities.FirstOrDefault(s => s.Id.Equals(stock.Id));

                        //foreach (var StockObj in StockObjList)
                        //{
                        if (StockObj.ItemQuantity <= quantity)
                        {
                            quantity -= StockObj.ItemQuantity;
                            StockObj.LocationId = stock.newLocation;
                            StockObj.StoreId = stock.newStore;
                            results = "No need for new barcode, everything moved to new location";
                            db.SaveChanges();
                        }
                        else
                        {
                            if (quantity > 0)
                            {
                                var per = (quantity / StockObj.ItemQuantity);
                                var newprice = StockObj.Price * per;

                                var splitteItem = db.StockQuantities.Add(new Models.StockQuantity
                                {
                                    ItemQuantity = quantity,
                                    DepartmentId = StockObj.DepartmentId,
                                    Price = newprice,//StockObj.Price,
                                    StockId = StockObj.StockId,
                                    DateCreated = DateTime.Now,
                                    DateModified = DateTime.Now,
                                    LocationId = stock.newLocation,
                                    StoreId = stock.newStore,
                                    BarcodePrinted = null,
                                    Splitted = true
                                }).Entity;
                                DbLogging(splitteItem, "Added(Splitting) New Stock Item By UOM");
                                StockObj.ItemQuantity -= quantity;

                                var perC = 1 - per;
                                var newpriceC = StockObj.Price * perC;

                                StockObj.Price = newpriceC;

                                StockObj.DateModified = DateTime.Now;
                                quantity = 0;
                                results = "New barcode required and has been created and waiting to be printed";
                                db.SaveChanges();
                                DbLogging(StockObj, "Updated Original Stock Item By UOM");
                            }
                            break;
                        }
                        //}

                        //TODO add stock log
                        break;
                    }
                case "SKU":
                    {

                        var stockQty = db.StockQuantities.FirstOrDefault(x => x.Id == stock.Id);

                        if (stock.PackQuantityMove > stockQty.ItemQuantity && stock.PackQuantityMove != stockQty.ItemQuantity)
                        {
                            throw new StockException("The Location does not have enough stock to transfer.");
                        }

                        stockQty.LocationId = stock.newLocation;
                        stockQty.StoreId = stock.newStore;
                        stockQty.DateModified = DateTime.Now;
                        db.Update(stockQty);
                        db.SaveChanges();
                        DbLogging(stockQty, "Moved Stock Item By SKU");
                        break;
                    }
                case "C_UoM":
                    {
                        var stockQty = db.StockQuantities
                             .Select(x => new DTO.StockQuantity
                             {
                                 Id= x.Id,
                                 uomid = stock.uomid,
                                 ItemQuantity = stock.PackQuantityMove,
                                 StockId = x.Stock.Id,
                             }).FirstOrDefault(x => x.Id == stock.Id);

                        stockQty.LocationId = stock.newLocation;
                        stockQty.StoreId = stock.newStore;
                        stockQty.ItemQuantity = stock.PackQuantityMove;

                        removeStockItem(stockQty);
                        results = stock.PackQuantityMove + " Has been consumed";
                        break;
                    }
            }
            return results;
        }
        //End Scanner
        public static DAL.DTO.Currency getCurrency(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            DAL.DTO.Currency currency = new DAL.DTO.Currency();
            currency.Iso = db.Suppliers.Where(l => l.Id == id).FirstOrDefault()?.Currency.Iso;

            return currency;
        }

        public static object getStockByBarcode(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var item = db.StockQuantities.Where(i => i.Id == id)
                .Select(s => new DAL.DTO.StockByBarcode
                {
                    Id = s.Id,
                    LocationId = s.LocationId,
                    DepartmentId = s.DepartmentId,
                    StoreId = s.StoreId,
                    StockId = s.StockId,
                    Code = s.Stock.Code,
                    StockFullName = s.Stock.ProductName + " - " + s.Stock.InternalProductName,
                    PackSize = s.Stock.PackSize,
                    ItemQuantity = s.ItemQuantity,
                    DateCreated = s.DateCreated,
                    DateModified = s.DateModified,
                    uomid = s.Stock.Uomid,
                    UomName = s.Stock.Uom.Name,
                    SupplierId = s.Stock.SupplierId,
                    SupplierFullname = s.Stock.Supplier.CompanyName,
                    Barcode = "STK" + s.Id.ToString(),
                    BarcodePrinted = s.BarcodePrinted,
                    Price = s.Price
                });

            return item;
        }
    }
}
