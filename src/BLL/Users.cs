using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Users
    {
        public static IQueryable<DAL.DTO.AllUserDetails> getUsers()
        {
            return DAL.Users.getUsers();
        }

        public static async Task<int> addUsers(string values)
        {
            return await DAL.Users.addUsers(values);
        }

        public static Task<int> editUsers(int key, string values)
        {
            return DAL.Users.editUsers(key, values);
        }

        public static int deactivateUser(DAL.DTO.ActivateUser user)
        {
            return DAL.Users.deactivateUser(user);
        }

        public static int activateUser(DAL.DTO.ActivateUser user)
        {
            return DAL.Users.activateUser(user);
        }

        public static IQueryable<DAL.DTO.User> getActiveUsers()
        {
            return DAL.Users.getActiveUsers();
        }

        public static int sendEmail(int id)
        {
            return DAL.Users.sendEmail(id);
        }

        public static int dontEmail(int id)
        {
            return DAL.Users.dontEmail(id);
        }
    }
}
