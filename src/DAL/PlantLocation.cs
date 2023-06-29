using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class PlantLocation
    {
        public static IQueryable<DAL.DTO.PlantLocation> getPlantLocation()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PlantLocations
               .Select(p => new DAL.DTO.PlantLocation
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   DefaultStoreId = p.DefaultStoreId,
                   Address = new DAL.DTO.PlantLocationAddress
                   {
                       StreetAddress1 = p.Address.StreetAddress1,
                       StreetAddress2 = p.Address.StreetAddress2,
                       Suburb = p.Address.Suburb,
                       City = p.Address.City,
                       CountryId = p.Address.CountryId,
                       PostCode = p.Address.PostCode,
                   },

               });
            return source;
        }

        public static int addPlantLocation(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.PlantLocation();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.PlantLocations.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new PlantLocationException("Plant Location already exists.");
            }
            db.PlantLocations.Add(Obj);
            db.SaveChanges();
            
            /*TODO: use Obj to add store*/
            DAL.Models.AISContext db2 = new DAL.Models.AISContext();
            int storeTypeId = db2.StoreTypes.Where(n => n.Name == "Virtual").FirstOrDefault().Id;
            string store = "{ 'name':'Production','description':'Production store for the above location.','storeTypeId':"+storeTypeId+",'plantLocationId':"+Obj.Id+ ",'productionStore':'true','quarantine':'false'}";
            var ObjStore = new DAL.Models.Store();
            JsonConvert.PopulateObject(store, ObjStore);

            db2.Stores.Add(ObjStore);
            db2.SaveChanges();

            return Obj.Id;
        }

        public static async Task<int> editPlantLocation(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.PlantLocations.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PlantLocationException("Plant Location does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.PlantLocations.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new PlantLocationException("Plant Location already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deletePlantLocation(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.PlantLocations.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PlantLocationException("Plant Location does not exist.");

            if (Obj.StockQuantities.Count > 0)
            {
                throw new PlantLocationException("The Location has stock assigned to it.");
            }

            if (Obj.Stocks.Count > 0)
            {
                throw new PlantLocationException("The Location is linked to a stock item.");
            }

            /*if (Obj.Stores.Count > 0)
            {
                throw new PlantLocationException("The Location has stores linked to it.");
            }*/

            if (Obj.InternalOrders.Count > 0)
            {
                throw new PlantLocationException("The Location is linked to a internal order.");
            }

            var StoreObj = await db.Stores.FirstOrDefaultAsync(o => o.PlantLocationId == key);
            if (StoreObj == null) throw new StoresException("Store does not exist.");

            if (StoreObj.Grnitems.Count > 0)
            {
                throw new PlantLocationException("The Location is linked to a GRN Item.");
            }

            Obj.DefaultStoreId = null;
            db.SaveChanges();
            db.Stores.RemoveRange(Obj.Stores);
            db.PlantLocations.Remove(Obj);
            db.Addresses.RemoveRange(Obj.Address);  
            
            await db.SaveChangesAsync();

            return "Plant Location Deleted";
        }

        public static IQueryable<DAL.DTO.PlantLocation> filterLocations(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var departments = db.StockQuantities.Where(t => t.StockId == id).Select(d => d.Location).Distinct()
            .Select(p => new DAL.DTO.PlantLocation
            {
                Id = p.Id,
                Name = p.Name,
                DefaultStoreId = p.DefaultStoreId
            });

            return departments;
        }

        public static object getDefaultStore(int locationId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.PlantLocations.Where(s => s.Id == locationId)
               .Select(p => new DAL.DTO.PlantLocation
               {
                   Id = p.Id,
                   DefaultStoreId = p.DefaultStoreId,
               });
            return source;
        }
    }
}
