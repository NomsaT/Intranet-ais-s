using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Stocktake
    {
        public static IQueryable<DAL.DTO.Stocktake> GetStocktake()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocktakes
                //.Where(x => x.CapturedQty != x.CurrentQty)
                .Include(x => x.Stock)
                .Include(y => y.PlantLocation)
                .Include(x => x.Store)
                .Include(y => y.User)
               .Select(p => new DAL.DTO.Stocktake
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   PlantLocationId = p.PlantLocationId,
                   StoreId = p.StoreId,
                   CapturedQty = p.CapturedQty,
                   CurrentQty = p.CurrentQty,
                   Recount  = p.Recount,
                   StockTakeDate = p.StockTakeDate,
                   Location = p.PlantLocation.Name,
                   Store = p.Store.Name,
                   Stock = p.Stock.Code + " - "+ p.Stock.InternalProductName,
                   UserId = p.UserId,
               });
            return source;
        }

        public async static Task<List<DAL.DTO.Stocktake>> GetRecounts(int userId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var recounts = await db.Stocktakes.Where(x => x.UserId == userId && x.CapturedQty == 0)
               .Select(p => new DAL.DTO.Stocktake
               {
                   Id = p.Id,
                   Stock = p.Stock.Code + " - " + p.Stock.InternalProductName,
                   Location = p.PlantLocation.Name,
                   Store = p.Store.Name,
                   CapturedQty = p.CapturedQty,
                   CurrentQty = p.CurrentQty,
                   Recount = p.Recount,
                   PlantLocationId = p.PlantLocationId,
                   StockId = p.StockId,
                   UserId = p.UserId,
                   StoreId = p.StoreId,
               }).ToListAsync();
            return recounts;    
        }

        public async static Task<Boolean> Stocktaking(List<DTO.Stocktake> list)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            if (list.Count() == 0)
            {
                throw new ArgumentException("No Stocktake...Please select stock");
            }
            var stocktakes = new List<DAL.Models.Stocktake>();

            foreach (var item in list)
            {
                var stocktake = db.Stocktakes.FirstOrDefault(x => x.StockId == item.StockId);
                var stockFromDb = db.Stocks.FirstOrDefault(x => x.Id == item.StockId);

                if (stocktake != null)
                {
                    stocktake.CapturedQty = item.CapturedQty == 0 ? item.CurrentQty : item.CapturedQty;
                    stocktake.Recount = false;
                    db.Stocktakes.Update(stocktake);
                    StockTakeLog(new List<Models.Stocktake> { stocktake }, "Updated");
                }
                else
                {
                    var stock = new DAL.Models.Stocktake
                    {
                        CurrentQty = item.CurrentQty,
                        CapturedQty = item.CapturedQty == 0 ? item.CurrentQty : item.CapturedQty,
                        PlantLocationId = item.PlantLocationId,
                        StockId = item.StockId,
                        StoreId = item.StoreId,
                        StockTakeDate = DateTime.Now,
                        Recount = false,
                        UserId = item.UserId,
                    };
                    db.Stocktakes.Add(stock);
                    stocktakes.Add(stock);
                }
            }
            db.SaveChanges();

            StockTakeLog(stocktakes, "Added");
            return true;
        }

        private static void StockTakeLog(List<Models.Stocktake> stocktakes, string action)
        {
            if (stocktakes.Count() < 0)
            {
                throw new Exception("Something went wrong");
            }

            using (var db = new DAL.Models.AISContext())
            {
                var stocktakeLogs = new List<Models.StocktakeLog>();
                foreach (var stocktake in stocktakes)
                {
                    var StocktakeFromDb = GetStocktake().FirstOrDefault(x => x.Id == stocktake.Id);
                    var user = db.Users.FirstOrDefault(x => x.Id == stocktake.UserId);
                    if (user == null) throw new Exception("User was not found");
                    var log = new Models.StocktakeLog
                    {
                        CountQty = stocktake.CapturedQty,
                        CurrentQty = stocktake.CurrentQty,
                        PlantLocationName = StocktakeFromDb.Location,
                        Recount = (bool)stocktake.Recount,
                        RecountDate = (DateTime)(stocktake.Recount == false ? SqlDateTime.Null : DateTime.Now),
                        ApproveDate = (DateTime)(stocktake.CurrentQty == stocktake.CapturedQty ? DateTime.Now : SqlDateTime.Null),
                        StockFullName = StocktakeFromDb.Stock,
                        StockTakeDate = (DateTime)stocktake.StockTakeDate,
                        StoreName = StocktakeFromDb.Store,
                        Actions = action,
                        UserFullName = user.Name + " " + user.Surname,
                    };
                    stocktakeLogs.Add(log);

                }
                db.StocktakeLogs.AddRange(stocktakeLogs);
                db.SaveChanges();
            }
        }

        public async static Task<DAL.DTO.Stocktake> ApproveStocktake(int id)
        {
            //DAL.Models.AISContext db = new DAL.Models.AISContext();
            List<DAL.Models.Stocktake> stocktakes = new List<Models.Stocktake>();
            List<DAL.Models.StockQuantity> stockQuantities = new List<Models.StockQuantity>();
            DAL.Models.Stocktake stockTake = null;

            using (var db = new DAL.Models.AISContext())
            {
                stockTake = await db.Stocktakes.FirstOrDefaultAsync(x => x.Id == id);

                if (stockTake != null)
                {
                    if (stockTake.CurrentQty > 0 && stockTake.CapturedQty > 0)
                    {
                        var qtySum = stockTake.Stock.StockQuantities
                        .Where(
                            x => x.StockId == stockTake.StockId &&
                            x.LocationId == stockTake.PlantLocationId &&
                            x.StoreId == stockTake.StoreId)
                        .Count();

                        var mainStock = stockTake.Stock;

                        if (stockTake.CapturedQty > qtySum)
                        {
                            //Current Qty 63 225  - 63 000
                            int diff = (int)(stockTake.CapturedQty - qtySum);

                            for (int i = 0; i < diff; i++)
                            {
                                var price = mainStock.ItemPrice / mainStock.PackSize;
                                var stockQuantity = new DAL.Models.StockQuantity
                                {
                                    ItemQuantity = mainStock.ItemQuantity,
                                    BarcodePrinted = false,
                                    DateCreated = DateTime.Now,
                                    DateModified = DateTime.Now,
                                    DepartmentId = mainStock.DefaultDepartmentId,
                                    LocationId = mainStock.DefaultLocationId,
                                    StoreId = mainStock.DefaultStoreId,
                                    Price = mainStock.CurrentPrice,
                                    Splitted = false,
                                    VerificationScan = false,
                                    StockId = mainStock.Id,
                                };
                                stockQuantities.Add(stockQuantity);
                                mainStock.ItemPrice += stockQuantity.Price;
                            }
                            StocktakeReport(ToDto(stockTake));
                            stocktakes.Add(stockTake);
                            stockTake.CurrentQty = stockTake.CapturedQty;
                            db.Stocktakes.Update(stockTake);
                            db.Stocks.Update(mainStock);
                            await db.StockQuantities.AddRangeAsync(stockQuantities);
                            StockTakeLog(stocktakes, "Approved");
                            db.Stocktakes.Remove(stockTake);
                        }
                        else if (stockTake.CapturedQty < qtySum)
                        {
                            int diff = (int)(qtySum - stockTake.CapturedQty);

                            var removeFromDb = db.StockQuantities
                                .Where(
                                    x => x.StockId == stockTake.StockId &&
                                    x.LocationId == stockTake.PlantLocationId &&
                                    x.StoreId == stockTake.StoreId)
                                .Take(diff)
                                .ToList();
                            StocktakeReport(ToDto(stockTake));
                            mainStock.ItemPrice -= mainStock.CurrentPrice * diff;
                            stockTake.CurrentQty = stockTake.CapturedQty;
                            db.Stocktakes.Update(stockTake);
                            stocktakes.Add(stockTake);
                            db.Stocks.Update(mainStock);
                            db.StockQuantities.RemoveRange(removeFromDb);
                            StockTakeLog(stocktakes, "Approved");
                            db.Stocktakes.Remove(stockTake);
                        }
                        else if(stockTake.CapturedQty == qtySum)
                        {
                            db.Stocktakes.Remove(stockTake);
                            StockTakeLog(new List<Models.Stocktake> { stockTake }, "Logged");
                            db.SaveChanges();
                            StocktakeReport(ToDto(stockTake));
                            throw new Exception("Captured Qty and Current Qty is the same.");
                        }
                    }
                    else if(stockTake.CapturedQty == 0)
                    {
                        throw new Exception("You can't approve because the item marked for recount");
                    }
                    else if (stockTake.CurrentQty == 0)
                    {
                        throw new Exception("This is stock does not have items");
                    }
                }
                else
                {
                    throw new Exception("Stocktake is not found");
                }
                await db.SaveChangesAsync();
                return ToDto(stockTake);
            }
        }

        public static int CalcDiff(int diff, decimal quantity)
        {
            return (int)(diff / quantity);
        }

        public async static Task<DAL.DTO.Stocktake> RecountStocktake(int id, int userId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var stockTake = await db.Stocktakes.FirstOrDefaultAsync(x => x.Id == id);

            if (stockTake == null)
            {
                throw new Exception("No stocktake to recount");
            }

            stockTake.CapturedQty = 0;
            stockTake.Recount = true;
            stockTake.UserId = userId;

            db.Stocktakes.Update(stockTake);
            await db.SaveChangesAsync();
            StockTakeLog(new List<Models.Stocktake> { stockTake }, "Mark for Recount");
            return new DTO.Stocktake
            {
                CapturedQty = stockTake.CapturedQty,
                CurrentQty = stockTake.CurrentQty,
                Id = id,
                Recount = true,
                UserId = stockTake.UserId
            };
        }

        public async static Task<List<DTO.StocktakeLog>> GetStocktakeLog()
        {
            using (var db = new DAL.Models.AISContext())
            {
                return await db.StocktakeLogs.Select(x => new DTO.StocktakeLog
                {
                    Id = x.Id,
                    CountQty = x.CountQty,
                    CurrentQty = x.CurrentQty,
                    PlantLocationName = x.PlantLocationName,
                    Recount = x.Recount,
                    RecountDate= (DateTime)x.RecountDate,
                    StockTakeDate= (DateTime)x.StockTakeDate,
                    ApproveDate= (DateTime)x.ApproveDate,
                    StockFullName= x.StockFullName,
                    StoreName= x.StoreName,
                    Actions = x.Actions,
                    UserFullName = x.UserFullName,

                }).ToListAsync();
            }
        }

        public static DAL.DTO.Stocktake ToDto(this DAL.Models.Stocktake stocktake)
        {
            var db = new DAL.Models.AISContext();
            if (stocktake != null)
            {
                if (stocktake.PlantLocation == null || stocktake.Stock == null || stocktake.Store == null || stocktake.User == null)
                {
                    stocktake.PlantLocation = db.PlantLocations.FirstOrDefault(x => x.Id == stocktake.PlantLocationId);
                    stocktake.Stock = db.Stocks.FirstOrDefault(x => x.Id == stocktake.StockId);
                    stocktake.Store = db.Stores.FirstOrDefault(x => x.Id == stocktake.StoreId);
                    stocktake.User = db.Users.FirstOrDefault(x => x.Id == stocktake.UserId);
                }
                var stock = new DAL.DTO.Stocktake
                {
                    CapturedQty = stocktake.CapturedQty,
                    CurrentQty = stocktake.CurrentQty,
                    Id = stocktake.Id,
                    Recount = stocktake.Recount,
                    PlantLocationId = stocktake.PlantLocationId,
                    StockId = stocktake.StockId,
                    StoreId = stocktake.StoreId,
                    Location = stocktake.PlantLocation.Name,
                    Stock = stocktake.Stock.Code + " - " + stocktake.Stock.InternalProductName,
                    Store = stocktake.Store.Name,
                    StockTakeDate = stocktake.StockTakeDate,
                    UserId = stocktake.UserId,
                };
                return stock;
            }

            return null;
        }

        public static List<DAL.Models.Stocktake> ToEntity(this List<DAL.DTO.Stocktake> stocktakes)
        {
            var list = new List<DAL.Models.Stocktake>();

            if (stocktakes != null)
            {
                foreach (var stocktake in stocktakes)
                {
                    var stock = new DAL.Models.Stocktake
                    {
                        CapturedQty = stocktake.CapturedQty == 0 ? stocktake.CurrentQty : stocktake.CapturedQty,
                        CurrentQty = stocktake.CurrentQty,
                        Id = stocktake.Id,
                        Recount = stocktake.Recount,
                        PlantLocationId = stocktake.PlantLocationId,
                        StockId = stocktake.StockId,
                        StoreId = stocktake.StoreId,
                    };

                    list.Add(stock);
                }
                return list;
            }

            return null;
        }

        public static DAL.Models.StocktakeReport ToStocktakeReport(this DAL.DTO.StocktakeReport input)
        {
            if (input != null)
            {
                var stocktakeReport = new DAL.Models.StocktakeReport
                {
                    Id = input.Id,
                    ClosingQty = input.ClosingQty,
                    OpeningQty = input.OpeningQty,
                    PlantLocationName = input.PlantLocationName,
                    StockFullName = input.StockFullName,
                    StocktakeCycleId = input.StocktakeCycleId,
                    StoreName = input.StoreName,
                };
                return stocktakeReport;
            }
            return null;
        }

        public static void StocktakeReport(DAL.DTO.Stocktake stockatakeReport)
        {
            using (var db = new DAL.Models.AISContext())
            {
                //var stocktakeReport = ToStocktakeReport(stockatakeReport);
                var stocktakeReport = new DAL.Models.StocktakeReport();

                if (IsBewteenTwoDates(stockatakeReport.StockTakeDate) != null)
                {
                    stocktakeReport = new DAL.Models.StocktakeReport
                    {
                        ClosingQty = stockatakeReport.CapturedQty,
                        OpeningQty = stockatakeReport.CurrentQty,
                        PlantLocationName = stockatakeReport.Location,
                        StockFullName = stockatakeReport.Stock,
                        StocktakeCycleId = IsBewteenTwoDates(stockatakeReport.StockTakeDate).Id,
                        StoreName = stockatakeReport.Store,
                    };
                }
                else
                {
                    var cycle = db.StocktakeCycles.Add(new Models.StocktakeCycle
                    {
                        EndDate = DateTime.Now,
                        StartDate= DateTime.Now,
                        StocktakeName = "Unscheduled " + AddStocktakeCounter(0, 1),
                    }).Entity;

                    db.SaveChanges();

                    stocktakeReport = new DAL.Models.StocktakeReport
                    {
                        ClosingQty = stockatakeReport.CapturedQty,
                        OpeningQty = stockatakeReport.CurrentQty,
                        PlantLocationName = stockatakeReport.Location,
                        StockFullName = stockatakeReport.Stock,
                        StocktakeCycleId = cycle.Id,
                        StoreName = stockatakeReport.Store,
                    };
                }

                db.StocktakeReports.Add(stocktakeReport);
                db.SaveChanges();
            }
        }

        private static int? AddStocktakeCounter(int? CycleCounter, int? UnScheduledCounter)
        {
            using (var context = new DAL.Models.AISContext())
            {
                Models.StocktakeCounter stocktakeCount = new Models.StocktakeCounter();

                var counters = context.StocktakeCounters.FirstOrDefault();

                if (counters != null)
                {
                    if (CycleCounter != 0)
                    {
                        CycleCounter = counters.CycleCounter + 1;
                        counters.CycleCounter = CycleCounter;
                        context.StocktakeCounters.Update(counters);
                        context.SaveChanges();
                        return CycleCounter;
                    }
                    else
                    {
                        UnScheduledCounter = counters.UnScheduledCounter + 1;
                        counters.UnScheduledCounter = UnScheduledCounter;
                        context.StocktakeCounters.Update(counters);
                        context.SaveChanges();
                        return UnScheduledCounter;
                    }
                }
                else
                {
                    stocktakeCount = new DAL.Models.StocktakeCounter { CycleCounter = CycleCounter, UnScheduledCounter = UnScheduledCounter };
                    var counter = context.StocktakeCounters.Add(stocktakeCount).Entity;
                    context.SaveChanges();
                    return counter.CycleCounter == 0 ? counter.UnScheduledCounter : counter.CycleCounter;
                }
            }
        }

        public static DAL.Models.StocktakeCycle IsBewteenTwoDates(this DateTime? dt)
        {
            using (var db = new DAL.Models.AISContext())
            {
                var t = db.StocktakeCycles.Where(x => dt >= x.StartDate && dt <= x.EndDate).FirstOrDefault();
                return t;
            }

        }

        public static IQueryable<DAL.DTO.StocktakeCycle> GetStocktakeCycle()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StocktakeCycles
               .Select(p => new DAL.DTO.StocktakeCycle
               {
                   Id = p.Id,
                   StartDate = p.StartDate,
                   EndDate = p.EndDate,
                   StocktakeName = p.StocktakeName
               });
            return source;
        }
    }
}
