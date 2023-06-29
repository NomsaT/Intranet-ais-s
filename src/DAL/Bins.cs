using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Bins
    {
        public static IQueryable<DAL.DTO.Bins> getBins()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Bins
                .Select(s => new DAL.DTO.Bins
                {
                    Id = s.Id,
                    Name = s.Name,                    
                    StoreId = s.StoreId,
                    StoreName = s.Store.Name,
                    PlantLocationId = s.Store.PlantLocationId,
                    PlantLocationName = s.Store.PlantLocation.Name,
                    Barcode = "BIN" + s.Id.ToString("D6")
                });
            return source;
        }

        public static int addBins(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = new DAL.Models.Bin();
            JsonConvert.PopulateObject(values, Obj);

            var check = db.Bins.Where(s => s.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new BinException("Bin already exists.");
            }                  

            db.Bins.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editBins(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Bins.FirstOrDefaultAsync(i => i.Id == key);
            if (Obj == null) throw new BinException("Bin does not exist.");           

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Bins.Where(m => m.Name == Obj.Name && m.Id != key).FirstOrDefault();
            if (check != null)
            {
                throw new BinException("Bin already exists.");
            }           

            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> deleteBins(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var BinObj = await db.Bins.FirstOrDefaultAsync(s => s.Id == key);
            if (BinObj == null) throw new BinException("Bin does not exist.");            

            if (BinObj.Stocks.Count > 0)
            {
                throw new BinException("Bin is linked to a stock item.");
            }
          
            db.Bins.Remove(BinObj);

            db.SaveChanges();

            return key;
        }
    }
}
