using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Vat
    {
        public static IQueryable<DAL.DTO.Vat> getVat()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Vats
               .Select(p => new DAL.DTO.Vat
               {
                   Id = p.Id,
                   Vat1 = p.Vat1

               });
            return source;
        }

        public static async Task<int> editVat(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Vats.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new VatException("VAT does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Vats.Where(m => m.Vat1 == Obj.Vat1 && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new VatException("VAT already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static object getVatPerc()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var vat = db.Vats.FirstOrDefault().Vat1;               
            return vat;
        }
    }
}
