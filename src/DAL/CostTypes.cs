using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class CostTypes
    {
        public static IQueryable<DAL.DTO.CostType> getCostTypes()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.CostTypes
               .Select(p => new DAL.DTO.CostType
               {
                   Id = p.Id,
                   Abbreviation = p.Abbreviation,
                   Name = p.Name,
                   Description = p.Description,
                   AbbName = p.Abbreviation + " - " + p.Name
               });
            return source;
        }

        public static int addCostTypes(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.CostType();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.CostTypes.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new CostTypeException("Name already exists.");
            }

            var checkCode = db.CostTypes.Where(m => m.Abbreviation == Obj.Abbreviation).FirstOrDefault();
            if (checkCode != null)
            {
                throw new CostTypeException("Abbreviation already exists.");
            }

            db.CostTypes.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editCostTypes(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.CostTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new CostTypeException("Cost Type does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.CostTypes.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new CostTypeException("Name already exists.");
            }

            var checkCode = db.CostTypes.Where(m => m.Abbreviation == Obj.Abbreviation && m.Id != Obj.Id).FirstOrDefault();
            if (checkCode != null)
            {
                throw new CostTypeException("Abbreviation already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteCostTypes(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.CostTypes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new CostTypeException("Cost Type does not exist.");

            db.CostTypes.Remove(Obj);
            await db.SaveChangesAsync();

            return "Cost Type Deleted";
        }
    }
}
