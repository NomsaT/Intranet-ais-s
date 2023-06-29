using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StoreTypes
    {
        public static IQueryable<DAL.DTO.StoreType> getStoreTypes()
        {
            return DAL.StoreTypes.getStoreTypes();
        }

        public static int addStoreTypes(string values)
        {
            return DAL.StoreTypes.addStoreTypes(values);
        }
        public static Task<int> editStoreTypes(int key, string values)
        {
            return DAL.StoreTypes.editStoreTypes(key, values);
        }

        public static Task<string> deleteStoreTypes(int key)
        {
            return DAL.StoreTypes.deleteStoreTypes(key);
        }
    }
}
