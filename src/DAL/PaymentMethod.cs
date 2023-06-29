using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class PaymentMethod
    {
        public static IQueryable<DAL.DTO.PaymentMethod> getPaymentMethod()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PaymentMethods
               .Select(p => new DAL.DTO.PaymentMethod
               {
                   Id = p.Id,
                   Type = p.Type,
                   Description = p.Description,
                   RequireBankingDetails = p.RequireBankingDetails

               });
            return source;
        }

        public static bool? getBankingDetailsRequired(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var required = db.PaymentMethods.Where(p => p.Id == id).FirstOrDefault()?.RequireBankingDetails;                        

            return required;
        }

        public static int addPaymentMethod(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.PaymentMethod();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.PaymentMethods.Where(m => m.Type == Obj.Type).FirstOrDefault();
            if (check != null)
            {
                throw new PaymentMethodsException("Payment Method already exists.");
            }
            db.PaymentMethods.Add(Obj);
            db.SaveChangesAsync();
            return Obj.Id;
        }

        public static async Task<int> editPaymentMethod(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.PaymentMethods.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PaymentMethodsException("Payment Method does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.PaymentMethods.Where(m => m.Type == Obj.Type && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new PaymentMethodsException("Payment Method already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deletePaymentMethod(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.PaymentMethods.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new PaymentMethodsException("Payment Method does not exist.");

            if (Obj.Customers.Count > 0)
            {
                throw new PaymentMethodsException("The Payment Method is assigned to a Customer.");
            }

            db.PaymentMethods.Remove(Obj);
            await db.SaveChangesAsync();

            return "Payment Method Deleted";
        }
    }
}
