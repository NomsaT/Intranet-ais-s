using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class EmployeePosition
    {
        public static IQueryable<DAL.DTO.EmployeePosition> getEmployeePosition()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.EmployeePositions
               .Select(p => new DAL.DTO.EmployeePosition
               {
                   Id = p.Id,
                   Position = p.Position,
                   Description = p.Description

               });
            return source;
        }

        public static int addEmployeePosition(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.EmployeePosition();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.EmployeePositions.Where(m => m.Position == Obj.Position).FirstOrDefault();
            if (check != null)
            {
                throw new EmployeePositionException("Position already exists.");
            }
            db.EmployeePositions.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editEmployeePosition(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.EmployeePositions.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new EmployeePositionException("Position does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.EmployeePositions.Where(m => m.Position == Obj.Position && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new EmployeePositionException("Position already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteEmployeePosition(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.EmployeePositions.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new EmployeePositionException("Position does not exist.");

            if (Obj.UserDetails.Count > 0)
            {
                throw new SupplierException("The position is assigned to an user.");
            }

            db.EmployeePositions.Remove(Obj);
            await db.SaveChangesAsync();

            return "Position Deleted";
        }
    }
}
