using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class DepartmentUsers
    {
        public static IQueryable<DAL.DTO.DepartmentUser> getDepartmentUsers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentUsers
               .Select(p => new DAL.DTO.DepartmentUser
               {
                   Id = p.Id,
                   UserId = p.UserId,
                   DepartmentId = p.DepartmentId,
                   Percentage = p.Percentage

               });
            return source;
        }

        public static int addDepartmentUsers(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.DepartmentUser();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.DepartmentUsers.Where(m => m.UserId == Obj.UserId && m.DepartmentId == Obj.DepartmentId).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentUsersException("User already allocated to the department.");
            }

            var totalPerc = db.DepartmentUsers.Where(s => s.UserId == Obj.UserId).Select(p => p.User.DepartmentUsers.Sum(e => e.Percentage)).FirstOrDefault();
            totalPerc = totalPerc + Obj.Percentage;
            if (totalPerc > 100)
            {
                throw new DepartmentUsersException("The Users allocation to the department is more than 100%");
            }
            db.DepartmentUsers.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editDepartmentUsers(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.DepartmentUsers.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentUsersException("User does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.DepartmentUsers.Where(m => m.UserId == Obj.UserId && m.DepartmentId == Obj.DepartmentId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentUsersException("User already allocated to the department.");
            }
            var currentfieldPrice = db.DepartmentUsers.Where(m => m.Id == Obj.Id).Select(p => p.Percentage).FirstOrDefault();
            var totalPerc = db.DepartmentUsers.Where(s => s.UserId == Obj.UserId).Select(p => p.User.DepartmentUsers.Sum(e => e.Percentage)).FirstOrDefault();
            totalPerc = totalPerc - currentfieldPrice;
            totalPerc = totalPerc + Obj.Percentage;
            if (totalPerc > 100)
            {
                throw new DepartmentUsersException("The Users allocation to the department is more than 100%");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteDepartmentUsers(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.DepartmentUsers.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentUsersException("Department for the user does not exist.");

            db.DepartmentUsers.Remove(Obj);
            await db.SaveChangesAsync();

            return "Department removed from this user";
        }
    }
}
