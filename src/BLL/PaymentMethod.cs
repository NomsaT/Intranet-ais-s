using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class PaymentMethod
    {
        public static IQueryable<DAL.DTO.PaymentMethod> getPaymentMethod()
        {
            return DAL.PaymentMethod.getPaymentMethod();
        }

        public static bool? getBankingDetailsRequired(int id)
        {
            return DAL.PaymentMethod.getBankingDetailsRequired(id);
        }
        public static int addPaymentMethod(string values)
        {
            return DAL.PaymentMethod.addPaymentMethod(values);
        }
        public static Task<int> editPaymentMethod(int key, string values)
        {
            return DAL.PaymentMethod.editPaymentMethod(key, values);
        }

        public static Task<string> deletePaymentMethod(int key)
        {
            return DAL.PaymentMethod.deletePaymentMethod(key);
        }
    }
}
