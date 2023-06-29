using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StockGroup
    {
        public static IQueryable<DAL.DTO.StockGroup> getStockGroup()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockGroups
               .Select(p => new DAL.DTO.StockGroup
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   MinThreshold = p.MinThreshold

               });
            return source;
        }

        public static int addStockGroup(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.StockGroup();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.StockGroups.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new StockGroupException("Stock Group already exists.");
            }
            db.StockGroups.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editStockGroup(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StockGroups.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockGroupException("Stock Group does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.StockGroups.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new StockGroupException("Stock Group already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteStockGroup(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StockGroups.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockGroupException("Stock Group does not exist.");

            if (Obj.Stocks.Count > 0)
            {
                throw new StockGroupException("The Stock Group is linked to stock items.");
            }

            db.StockGroups.Remove(Obj);
            await db.SaveChangesAsync();

            return "Stock Group Deleted";
        }
    }
}
