using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Vat
    {
        public static IQueryable<DAL.DTO.Vat> getVat()
        {
            return DAL.Vat.getVat();
        }
        public static Task<int> editVat(int key, string values)
        {
            return DAL.Vat.editVat(key, values);
        }

        public static object getVatPerc()
        {
            return DAL.Vat.getVatPerc();
        }
    }
}
