using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Supplier
    {
        public static IQueryable<DAL.DTO.Supplier> getSupplier()
        {
            return DAL.Supplier.getSupplier();
        }
        public static int addSupplier(string values)
        {
            return DAL.Supplier.addSupplier(values);
        }

        public static Task<int> editSupplier(int key, string values)
        {
            return DAL.Supplier.editSupplier(key, values);
        }

        public static Task<int> deleteSupplier(int key)
        {
            return DAL.Supplier.deleteSupplier(key);
        }
    }
}
