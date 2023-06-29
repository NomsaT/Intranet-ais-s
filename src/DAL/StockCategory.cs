using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StockCategory
    {
        public static IQueryable<DAL.DTO.StockCategory> getStockCategory()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockCategories
               .Select(p => new DAL.DTO.StockCategory
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description

               });
            return source;
        }

        public static int addStockCategory(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.StockCategory();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.StockCategories.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new StockCategoryException("Stock Category already exists.");
            }
            db.StockCategories.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editStockCategory(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.StockCategories.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockCategoryException("Stock Category does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.StockCategories.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new StockCategoryException("Stock Category already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteStockCategory(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            /*TODO: RECIPE*/
            if (key == 1009)           
            {
                throw new StockCategoryException("The Stock Category cannot be deleted. It is used to mix stock items.");
            }

            var Obj = await db.StockCategories.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new StockCategoryException("Stock Category does not exist.");

            if (Obj.Stocks.Count > 0)
            {
                throw new StockCategoryException("The Stock Category is assigned to stock.");
            }

            db.StockCategories.Remove(Obj);
            await db.SaveChangesAsync();

            return "Stock Category Deleted";
        }
    }
}
