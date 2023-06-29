using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StorageType
    {
        public static IQueryable<DAL.DTO.StorageType> getStorageType()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StorageTypes
               .Select(p => new DAL.DTO.StorageType
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   RequireBarcode = p.RequireBarcode

               });
            return source;
        }

        public static int addStorageType(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.StorageType();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.StorageTypes.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new StorageTypeException("Storage Type already exists.");
            }
            db.StorageTypes.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editStorageType(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StorageTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StorageTypeException("Storage Type does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.StorageTypes.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new StorageTypeException("Storage Type already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteStorageType(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StorageTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StorageTypeException("Storage Type does not exist.");

            if (Obj.Stocks.Count > 0)
            {
                throw new StorageTypeException("The Storage Type is assigned to a Stock Item.");
            }

            db.StorageTypes.Remove(Obj);
            await db.SaveChangesAsync();

            return "Storage Type Deleted";
        }
    }
}
