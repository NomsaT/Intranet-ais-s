using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Roles
    {
        public static IQueryable<DAL.DTO.RoleTemplate> getRoles()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.RoleTemplates
               .Select(p => new DAL.DTO.RoleTemplate
               {
                   Id = p.Id,
                   Name = p.Name

               });
            return source;
        }

        public static int addRoles(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.RoleTemplate();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.RoleTemplates.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new RolesException("Role already exists.");
            }
            db.RoleTemplates.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editRoles(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.RoleTemplates.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new RolesException("Role does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.RoleTemplates.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new RolesException("Role already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteRoles(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.RoleTemplates.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new RolesException("Role does not exist.");

            if (Obj.RolePermissions.Count > 0)
            {
                throw new RolesException("The Role is being used in a Role Blueprint, remove it from the blueprint before deleting it.");
            }

            db.RoleTemplates.Remove(Obj);
            await db.SaveChangesAsync();

            return "Role Deleted";
        }

        public static object getRoleFirst()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.RoleTemplates
               .Select(p => new DAL.DTO.RoleTemplate
               {
                   Id = p.Id,
                   Name = p.Name
               });
            return source;
        }
    }
}
