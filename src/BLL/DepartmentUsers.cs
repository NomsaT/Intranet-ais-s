using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class DepartmentUsers
    {
        public static IQueryable<DAL.DTO.DepartmentUser> getDepartmentUsers()
        {
            return DAL.DepartmentUsers.getDepartmentUsers();
        }

        public static int addDepartmentUsers(string values)
        {
            return DAL.DepartmentUsers.addDepartmentUsers(values);
        }
        public static Task<int> editDepartmentUsers(int key, string values)
        {
            return DAL.DepartmentUsers.editDepartmentUsers(key, values);
        }

        public static Task<string> deleteDepartmentUsers(int key)
        {
            return DAL.DepartmentUsers.deleteDepartmentUsers(key);
        }
    }
}
