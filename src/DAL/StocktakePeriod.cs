using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class StocktakePeriod
    {

        public static int AddStocktakePeriod(string values)
        {
            using (var db = new DAL.Models.AISContext())
            {
                var stocktakeCycle = new DAL.Models.StocktakeCycle();
                JsonConvert.PopulateObject(values, stocktakeCycle);

                if (db.StocktakeCycles.ToList().Count == 0)
                {
                    stocktakeCycle.StartDate = stocktakeCycle.StartDate.AddDays(1);
                    stocktakeCycle.EndDate = stocktakeCycle.EndDate.AddDays(1);
                }

                if (dateVerify(stocktakeCycle.StartDate, stocktakeCycle.EndDate, -1)) // -1 for default compararison
                {
                    if (stocktakeCycle.StartDate > stocktakeCycle.EndDate)
                    {
                        throw new Exception("Start date cannot be greater than End date");
                    }
                    int? cycleCount = AddStocktakeCounter(1, 0);
                    var cycle = new DAL.Models.StocktakeCycle
                    {
                        StocktakeName = "Stocktake " + cycleCount,
                        EndDate = stocktakeCycle.EndDate.Date,
                        StartDate = stocktakeCycle.StartDate.Date,
                    };

                    db.StocktakeCycles.Add(cycle);
                    db.SaveChanges();
                    return cycle.Id;
                }
                return 0;
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

        public async static Task<object> DeleteStocktakePeriodAsync(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StocktakeCycles.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new Exception("Stocktake period does not exist.");

            var reports = db.StocktakeReports.Where(x => x.StocktakeCycleId == Obj.Id);

            if (reports == null)
            {
                throw new Exception("StocktakeReport does not exist.");
            }

            db.RemoveRange(reports);


            var counterId = 0;
            var unscheduledId = 0;
            if (Obj.StocktakeName.StartsWith("S"))
            {
                counterId = Int32.Parse(string.Join("", Obj.StocktakeName.ToCharArray().Where(Char.IsDigit)));
                var s = db.StocktakeCounters.FirstOrDefault();

                s.CycleCounter -= counterId;

                db.StocktakeCounters.Update(s);
                s.CycleCounter = 0;

                db.StocktakeCycles.Where(x => x.Id != Obj.Id && x.StocktakeName.StartsWith("S")).ToList().ForEach(x =>
                {
                    int c = (int)(s.CycleCounter += 1);
                    x.StocktakeName = "Stocktake " + c;
                });
            }
            else if (Obj.StocktakeName.StartsWith("U"))
            {
                unscheduledId = Int32.Parse(string.Join("", Obj.StocktakeName.ToCharArray().Where(Char.IsDigit)));
                var s = db.StocktakeCounters.FirstOrDefault();

                s.UnScheduledCounter -= unscheduledId;

                db.StocktakeCounters.Update(s);
                s.UnScheduledCounter = 0;

                db.StocktakeCycles.Where(x => x.Id != Obj.Id && x.StocktakeName.StartsWith("U")).ToList().ForEach(x =>
                {
                    int c = (int)(s.UnScheduledCounter += 1);
                    x.StocktakeName = "Unscheduled " + c;
                });
            }

            db.StocktakeCycles.Remove(Obj);
            await db.SaveChangesAsync();

            return "Period deleted";
        }

        public async static Task<object> EditStocktakePeriodsync(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StocktakeCycles.FirstOrDefaultAsync(o => o.Id == key);

            JsonConvert.PopulateObject(values, Obj);
            if (Obj == null) throw new Exception("Stocktake period does not exist.");
            if (dateEdit(Obj.StartDate, Obj.EndDate, Obj.Id))
            {
                db.StocktakeCycles.Update(Obj);
                await db.SaveChangesAsync();

                return Obj.Id;
            }
            return 0;
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

        public static bool dateVerify(DateTime start, DateTime end, int id)
        {

            using (var db = new DAL.Models.AISContext())
            {
                if (end.Date < start.Date)
                {
                    throw new Exception("End date cannot be less start date");
                }
                var validation1 = db.StocktakeCycles.Where(x => start.Date >= x.StartDate.Date && end.Date <= x.EndDate.Date && x.Id!= id).ToList();//tested
                var validation2 = db.StocktakeCycles.Where(x => start.Date <= x.StartDate.Date && end.Date >= x.EndDate.Date && x.Id != id).ToList();//tested
                var validation3 = db.StocktakeCycles.Where(x => start.Date >= x.StartDate.Date && start.Date <= x.EndDate.Date && x.Id != id).ToList();//tested
                var validation4 = db.StocktakeCycles.Where(x => start.Date == x.StartDate.Date && end.Date == x.EndDate.Date && x.Id != id).ToList();//tested

                if (validation1.Count > 0 || validation2.Count > 0 || validation3.Count > 0 || validation4.Count > 0)
                {
                    throw new Exception("Range already chosen.");
                }
 
                return true;
            }
        }

        public static bool dateEdit(DateTime startDate,DateTime endDate, int id)
        { 
            using (var db = new DAL.Models.AISContext())
            {
                return dateVerify(startDate, endDate, id );
            }
        }
    }
}