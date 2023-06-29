using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class UnitOfMeasurement
    {
        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUnitOfMeasurement()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.UnitOfMeasurements
               .Select(p => new DAL.DTO.UnitOfMeasurement
               {
                   Id = p.Id,
                   Name = p.Name

               });
            return source;
        }

        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUOMbyStock(int stockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stockId);
            var source = db.UnitOfMeasurements.Where(w => w.Id == mainStock.Uomid || w.Id == mainStock.SecondaryUomid)
                .Select(p => new DAL.DTO.UnitOfMeasurement
                {
                    Id = p.Id,
                    Name = p.Name
                });
            return source;
        }

        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUOMbyStockConsume(int itemStockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var stockId = db.StockQuantities.FirstOrDefault(i => i.Id == itemStockId).StockId;
            var mainStock = db.Stocks.FirstOrDefault(s => s.Id == stockId);
            var source = db.UnitOfMeasurements.Where(w => w.Id == mainStock.Uomid)
                .Select(p => new DAL.DTO.UnitOfMeasurement
                {
                    Id = p.Id,
                    Name = p.Name
                });
            return source;
        }

        public static int addUnitOfMeasurement(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.UnitOfMeasurement();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.UnitOfMeasurements.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new UnitOfMeasurementException("UOM already exists.");
            }
            db.UnitOfMeasurements.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editUnitOfMeasurement(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.UnitOfMeasurements.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new UnitOfMeasurementException("UOM does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.UnitOfMeasurements.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new UnitOfMeasurementException("UOM already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteUnitOfMeasurement(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.UnitOfMeasurements.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new UnitOfMeasurementException("UOM does not exist.");

            if (Obj.StockUoms.Count > 0)
            {
                throw new UnitOfMeasurementException("The UOM is assigned to stock.");
            }

            if (Obj.StockSecondaryUoms.Count > 0)
            {
                throw new UnitOfMeasurementException("The UOM is assigned to stock.");
            }

            db.UnitOfMeasurements.Remove(Obj);
            await db.SaveChangesAsync();

            return "UOM Deleted";
        }
    }
}
