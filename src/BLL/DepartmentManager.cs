using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class DepartmentManager
    {
        public static IQueryable<DAL.DTO.DepartmentManager> getDepartmentManager()
        {
            return DAL.DepartmentManager.getDepartmentManager();
        }

        public static int addDepartmentManager(string values)
        {
            return DAL.DepartmentManager.addDepartmentManager(values);
        }
        public static Task<int> editDepartmentManager(int key, string values)
        {
            return DAL.DepartmentManager.editDepartmentManager(key, values);
        }

        public static Task<string> deleteDepartmentManager(int key)
        {
            return DAL.DepartmentManager.deleteDepartmentManager(key);
        }
    }
}
