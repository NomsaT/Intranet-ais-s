using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Stores
    {
        public static IQueryable<DAL.DTO.Store> getStores()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stores
               .Select(p => new DAL.DTO.Store
               {
                   Id = p.Id,
                   Name = (p.Id == p.PlantLocation.DefaultStoreId) ? p.Name + " (Default Store)" : p.Name,
                   Description = p.Description,
                   PlantLocationId = p.PlantLocationId,
                   StoreTypeId = p.StoreTypeId,
                   PlantLocation = p.Name + " ("+p.PlantLocation.Name+")",
                   Quarantine = p.Quarantine,
               });
            return source;
        }
        public static IQueryable<DAL.DTO.Store> filterStore(int id,int locationId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var stores = db.StockQuantities.Where(t => t.StockId == id && t.LocationId == locationId).Select(s => s.Store).Distinct()
            .Select(p => new DAL.DTO.Store
            {
                Id = p.Id,
                Name = p.Name,
            });

            return stores;
        }

        public static IQueryable<DAL.DTO.Store> getBinStores()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stores
                .Where(b => b.Name != "Production")
               .Select(p => new DAL.DTO.Store
               {
                   Id = p.Id,
                   Name = (p.Id == p.PlantLocation.DefaultStoreId) ? p.Name + " (Default Store)" : p.Name,
                   Description = p.Description,
                   PlantLocationId = p.PlantLocationId,
                   StoreTypeId = p.StoreTypeId,
                   PlantLocation = p.Name + " (" + p.PlantLocation.Name + ")",
                   Quarantine = p.Quarantine,
               });
            return source;
        }

        public static int addStores(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Store();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.Stores.Where(m => m.Name == Obj.Name && m.PlantLocationId == Obj.PlantLocationId).FirstOrDefault();
            if (check != null)
            {
                throw new StoresException("Store already exists.");
            }

            Obj.ProductionStore = false;
            
            if (Obj.Quarantine == null)
            {
                Obj.Quarantine = false;
            }

            db.Stores.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editStores(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Stores.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StoresException("Store does not exist.");

            JsonConvert.PopulateObject(values, Obj);

            int ProdStoreId = db.Stores.Where(n => n.Name == "Production").FirstOrDefault().Id;
            if (ProdStoreId == key)
            {
                throw new StoresException("The production store cannot be edited.");
            }

            var check = db.Stores.Where(m => m.Name == Obj.Name && m.Id != Obj.Id && m.PlantLocationId == Obj.PlantLocationId).FirstOrDefault();
            if (check != null)
            {
                throw new StoresException("Store already exists.");
            }

            if (Obj.Quarantine == null)
            {
                Obj.Quarantine = false;
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteStores(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Stores.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StoresException("Store does not exist.");

            int ProdStoreId = db.Stores.Where(n => n.Name == "Production").FirstOrDefault().Id;
            if (ProdStoreId == key)
            {
                throw new StoresException("The production store cannot be removed.");
            }

            var defaultStoreID = db.PlantLocations.Where(p => p.Id == Obj.PlantLocationId).Select(s => s.DefaultStoreId).FirstOrDefault();
            if(defaultStoreID == key)
            {
                throw new StoresException("The store is set as a default store for the current plant location.");
            }

            if (Obj.StockQuantities.Count > 0)
            {
                throw new StoresException("The Store has stock assigned to it.");
            }

            if (Obj.Stocks.Count > 0)
            {
                throw new StoresException("The Store is linked to a stock item.");
            }

            if (Obj.Grnitems.Count > 0)
            {
                throw new StoresException("The Store is linked to a GRN Item.");
            }

            db.Stores.Remove(Obj);
            await db.SaveChangesAsync();

            return "Store Deleted";
        }
    }
}
