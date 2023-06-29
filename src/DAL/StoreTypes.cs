using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StoreTypes
    {
        public static IQueryable<DAL.DTO.StoreType> getStoreTypes()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StoreTypes
               .Select(p => new DAL.DTO.StoreType
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description
               });
            return source;
        }

        public static int addStoreTypes(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.StoreType();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.StoreTypes.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new StoreTypesException("Store Type already exists.");
            }
            db.StoreTypes.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editStoreTypes(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StoreTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StoreTypesException("Store Type does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.StoreTypes.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new StoreTypesException("Store Type already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteStoreTypes(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StoreTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StoreTypesException("Store Type does not exist.");

            db.StoreTypes.Remove(Obj);
            await db.SaveChangesAsync();

            return "Store Type Deleted";
        }
    }
}
