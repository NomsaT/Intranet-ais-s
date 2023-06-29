using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class GLCodes
    {
        public static IQueryable<DAL.DTO.Glcode> getGLCodes()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Glcodes
               .Select(p => new DAL.DTO.Glcode
               {
                   Id = p.Id,
                   Code = p.Code,
                   Name = p.Name,
                   Description = p.Description,
                   Glfullname = p.Code + " - " + p.Name,
                   AssignVat = p.AssignVat
               }).Where(a => a.AssignVat != true)
               .OrderBy(d => d.Code);
            return source;
        }

        public static int addGLCodes(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Glcode();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.Glcodes.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new GLCodeException("Name already exists.");
            }

            var checkCode = db.Glcodes.Where(m => m.Code == Obj.Code).FirstOrDefault();
            if (checkCode != null)
            {
                throw new GLCodeException("GL Code already exists.");
            }
            Obj.AssignVat = false;
            db.Glcodes.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editGLCodes(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Glcodes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new GLCodeException("GL Code does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Glcodes.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new GLCodeException("Name already exists.");
            }

            var checkCode = db.Glcodes.Where(m => m.Code == Obj.Code && m.Id != Obj.Id).FirstOrDefault();
            if (checkCode != null)
            {
                throw new GLCodeException("GL Code already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteGLCodes(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Glcodes.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new GLCodeException("GL Code does not exist.");
            
            var vatGL = db.Glcodes.Where(g => g.AssignVat == true).FirstOrDefault().Id;
            if(vatGL == Obj.Id)
            {
                throw new GLCodeException("The GL Code cannot be deleted, it is used to assign VAT to.");
            }

            db.Glcodes.Remove(Obj);
            await db.SaveChangesAsync();

            return "GL Code Deleted";
        }
    }
}
