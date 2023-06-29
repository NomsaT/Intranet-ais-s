using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Roles
    {
        public static IQueryable<DAL.DTO.RoleTemplate> getRoles()
        {
            return DAL.Roles.getRoles();
        }

        public static int addRoles(string values)
        {
            return DAL.Roles.addRoles(values);
        }
        public static Task<int> editRoles(int key, string values)
        {
            return DAL.Roles.editRoles(key, values);
        }

        public static Task<string> deleteRoles(int key)
        {
            return DAL.Roles.deleteRoles(key);
        }

        public static object getRoleFirst()
        {
            return DAL.Roles.getRoleFirst();
        }
    }
}
