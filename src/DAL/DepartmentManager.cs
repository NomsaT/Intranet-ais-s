using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class DepartmentManager
    {
        public static IQueryable<DAL.DTO.DepartmentManager> getDepartmentManager()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentManagers
               .Select(p => new DAL.DTO.DepartmentManager
               {
                   Id = p.Id,
                   UserId = p.UserId,
                   DepartmentId = p.DepartmentId

               });
            return source;
        }

        public static int addDepartmentManager(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.DepartmentManager();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.DepartmentManagers.Where(m => m.UserId == Obj.UserId && m.DepartmentId == Obj.DepartmentId).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentManagerException("Department Manager already exists.");
            }
            db.DepartmentManagers.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editDepartmentManager(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.DepartmentManagers.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentManagerException("Department Manager does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.DepartmentManagers.Where(m => m.UserId == Obj.UserId && m.DepartmentId == Obj.DepartmentId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentManagerException("Department Manager already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteDepartmentManager(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.DepartmentManagers.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentManagerException("Department Manager does not exist.");

            db.DepartmentManagers.Remove(Obj);
            await db.SaveChangesAsync();

            return "Department Manager Deleted";
        }
    }
}
