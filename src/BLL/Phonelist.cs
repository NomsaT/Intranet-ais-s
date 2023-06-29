using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Phonelist
    {
        public static IQueryable<DAL.DTO.Phonelist> getPhonelistSuppliers()
        {
            return DAL.Phonelist.getPhonelistSuppliers();
        }

        public static IQueryable<DAL.DTO.Phonelist> getPhonelistCustomers()
        {
            return DAL.Phonelist.getPhonelistCustomers();
        }
    }
}
