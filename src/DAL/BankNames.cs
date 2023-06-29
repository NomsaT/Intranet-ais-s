using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class BankNames
    {
        public static IQueryable<DAL.DTO.BankName> getBankNames()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.BankNames
               .Select(p => new DAL.DTO.BankName
               {
                   Id = p.Id,
                   BankName1 = p.BankName1,
                   Description = p.Description
               });
            return source;
        }

        public static int addBankNames(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.BankName();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.BankNames.Where(m => m.BankName1 == Obj.BankName1).FirstOrDefault();
            if (check != null)
            {
                throw new BankNameException("Bank Name already exists.");
            }

            db.BankNames.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editBankNames(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.BankNames.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new BankNameException("Bank Name does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.BankNames.Where(m => m.BankName1 == Obj.BankName1 && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new BankNameException("Bank Name already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteBankNames(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.BankNames.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new BankNameException("Bank Name does not exist.");

            db.BankNames.Remove(Obj);
            await db.SaveChangesAsync();

            return "Bank Name Deleted";
        }
    }
}
