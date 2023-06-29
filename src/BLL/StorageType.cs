using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StorageType
    {
        public static IQueryable<DAL.DTO.StorageType> getStorageType()
        {
            return DAL.StorageType.getStorageType();
        }

        public static int addStorageType(string values)
        {
            return DAL.StorageType.addStorageType(values);
        }
        public static Task<int> editStorageType(int key, string values)
        {
            return DAL.StorageType.editStorageType(key, values);
        }

        public static Task<string> deleteStorageType(int key)
        {
            return DAL.StorageType.deleteStorageType(key);
        }
    }
}
