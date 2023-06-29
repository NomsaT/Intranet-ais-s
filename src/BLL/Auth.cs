using System.Threading.Tasks;

namespace BLL
{
    public static class Auth
    {
        public static DAL.DTO.UserInfo login(DAL.DTO.Authenticate user)
        {
            return DAL.Auth.login(user);
        }

        public static async Task<int> ResetPassword(DAL.DTO.Authenticate user)
        {
            return await DAL.Auth.ResetPassword(user);
        }

        public static void BlockUser(DAL.DTO.BlockUser user)
        {
            DAL.Auth.BlockUser(user);
        }

        public static void SetPassword(DAL.DTO.ManageAuth user)
        {
            DAL.Auth.SetPassword(user);
        }

        public static void AdminSetPassword(DAL.DTO.SetPassword user)
        {
            DAL.Auth.AdminSetPassword(user);
        }

        public static void UserSetPassword(DAL.DTO.SetPassword user)
        {
            DAL.Auth.UserSetPassword(user);
        }
    }
}
