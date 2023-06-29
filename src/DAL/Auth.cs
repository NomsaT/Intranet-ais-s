using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Auth
    {
        public static DAL.DTO.UserInfo login(DAL.DTO.Authenticate user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            if (user == null)
            {
                throw new AuthException("No Authenticating Details");
            }

            var User = db.Users.Where(r => r.UserName == user.Account || r.UserDetails.First().Email == user.Account).FirstOrDefault();
            if (User == null)
            {
                throw new AuthException("Invalid Username/Email");
            }

            if (User.IsDisabled)
            {
                throw new AuthException("User Disabled/Blocked");
            }

            if (string.IsNullOrEmpty(User.Password))
            {
                User.Password = PasswordHash.HashPassword(user.Password);
                db.SaveChanges();
            }

            if (!PasswordHash.Verify(user.Password, User.Password))
            {
                throw new AuthException("Invalid Username/Email or Password");
            }

            /*User.LastActivity = DateTime.Now;*/
            db.SaveChanges();

            DAL.DTO.UserInfo userInfo = new DTO.UserInfo();

            userInfo.Id = User.Id;
            userInfo.Name = User.Name;
            userInfo.Surname = User.Surname;
            userInfo.Username = User.UserName;

            userInfo.Permissions = User.UserPermissions.Select(r => r.PermissionId).ToList();

            userInfo.Token = TokenGenerator.GenerateToken(userInfo.Permissions, userInfo.Id);

            return userInfo;
        }

        public static async Task<int> ResetPassword(DTO.Authenticate user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            if (user == null)
            {
                throw new AuthException("No Account Details");
            }

            var User = db.Users.Where(r => r.UserName == user.Account || r.UserDetails.First().Email == user.Account).FirstOrDefault();
            if (User == null)
            {
                throw new AuthException("Invalid Account Email");
            }

            User.ResetDateTime = DateTime.Now;
            db.SaveChanges();

            string DisplayName = string.Format("{0} {1} ({2})", User.Name, User.Surname, User.UserName);
            string LinkParam = string.Format("?Account={0}&Reference={1}", User.UserDetails.First().Email, ((DateTime)User.ResetDateTime).ToString("yyyyMMddHHmmss"));

            bool emailResult = await Task.FromResult(DAL.Classes.EmailLogic.sendEmailHtml(User.UserDetails.First().Email, new System.Collections.Generic.List<string>(), "ACS Set Password", DAL.Classes.EmailBodies.emailSetPasswordBody(DisplayName, LinkParam)));

            if (!emailResult)
            {
                throw new AuthException("Emailing Failed");
            }

            return User.Id;
        }

        public static void BlockUser(DTO.BlockUser user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            if (user == null)
            {
                throw new AuthException("No Account Details");
            }

            var User = db.Users.Where(r => r.UserName == user.Account || r.UserDetails.First().Email == user.Account).FirstOrDefault();
            if (User == null)
            {
                throw new AuthException("Invalid Username/Email");
            }

            User.IsDisabled = true;
            db.SaveChanges();

        }

        public static void SetPassword(DTO.ManageAuth user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            if (user == null)
            {
                throw new AuthException("No Account Details");
            }

            var User = db.Users.Where(r => r.UserName == user.Account || r.UserDetails.First().Email == user.Account).FirstOrDefault();
            if (User == null)
            {
                throw new AuthException("Invalid Username/Email");
            }

            if (!User.ResetDateTime.HasValue)
            {
                throw new AuthException("Invalid Set Password Link");
            }

            if (User.ResetDateTime.Value < DateTime.Now.AddDays(-1))
            {
                throw new AuthException("Expired Set Password Link");
            }

            if (User.ResetDateTime != null && ((DateTime)User.ResetDateTime).ToString("yyyyMMddHHmmss") == user.Reference)
            {
                User.Password = PasswordHash.HashPassword(user.Password);
                User.ResetDateTime = null;
                db.SaveChanges();
            }
            else
            {
                throw new AuthException("Invalid Set Password Link");
            }

        }

        public static void AdminSetPassword(DTO.SetPassword user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var User = db.Users.Find(user.Id);
            if (User == null)
            {
                throw new AuthException("Invalid Account");
            }

            User.Password = PasswordHash.HashPassword(user.Password);
            db.SaveChanges();
        }

        public static void UserSetPassword(DTO.SetPassword user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var User = db.Users.Find(user.Id);
            if (User == null)
            {
                throw new AuthException("Invalid Account");
            }

            User.Password = PasswordHash.HashPassword(user.Password);
            db.SaveChanges();
        }
    }
}
