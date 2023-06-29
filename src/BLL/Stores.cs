using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Stores
    {
        public static IQueryable<DAL.DTO.Store> getStores()
        {
            return DAL.Stores.getStores();
        }
        public static IQueryable<DAL.DTO.Store> filterStore(int id, int locationId)
        {
            return DAL.Stores.filterStore(id,locationId);
        }

        public static IQueryable<DAL.DTO.Store> getBinStores()
        {
            return DAL.Stores.getBinStores();
        }

        public static int addStores(string values)
        {
            return DAL.Stores.addStores(values);
        }
        public static Task<int> editStores(int key, string values)
        {
            return DAL.Stores.editStores(key, values);
        }

        public static Task<string> deleteStores(int key)
        {
            return DAL.Stores.deleteStores(key);
        }
    }
}
