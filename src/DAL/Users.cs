using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Users
    {
        public static IQueryable<DAL.DTO.AllUserDetails> getUsers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.UserDetails
               .Select(p => new DAL.DTO.AllUserDetails
               {
                   Id = p.User.Id,
                   UserDetailsId = p.Id,
                   Name = p.User.Name,
                   Surname = p.User.Surname,
                   UserName = p.User.UserName,
                   LastActivity = p.User.LastActivity ?? DateTime.Now,
                   IsDisabled = p.User.IsDisabled,
                   Idnumber = p.Idnumber,
                   EmployeeNumber = p.EmployeeNumber,
                   ContactNumber = p.ContactNumber,
                   Birthday = p.Birthday,
                   IncomeTaxNumber = p.IncomeTaxNumber,
                   ResetDateTime = p.User.ResetDateTime,
                   RaceId = p.RaceId,
                   GenderId = p.GenderId,
                   HomeAddressId = p.HomeAddressId,
                   StreetAddress1 = p.HomeAddress.StreetAddress1,
                   StreetAddress2 = p.HomeAddress.StreetAddress2,
                   Suburb = p.HomeAddress.Suburb,
                   City = p.HomeAddress.City,
                   PostCode = p.HomeAddress.PostCode,
                   CountryId = p.HomeAddress.CountryId,
                   NextOfKinName = p.NextOfKinName,
                   NextOfKinContactNumber = p.NextOfKinContactNumber,
                   BankNameId = p.BankNameId,
                   AccountNumber = p.AccountNumber,
                   BranchCode = p.BranchCode,
                   BaseSalaryPerMonth = p.BaseSalaryPerMonth,
                   HourlyRate = p.HourlyRate,
                   HighestQualification = p.HighestQualification,
                   MaritalStatusId = p.MaritalStatusId,
                   NumberOfDependants = p.NumberOfDependants,
                   TypeOfEmploymentId = p.TypeOfEmploymentId,
                   Email = p.Email,
                   StartDate = p.StartDate,
                   EmployeePositionId = p.EmployeePositionId,
                   Position = p.EmployeePosition.Position,
                   PositionDescription = p.EmployeePosition.Description,
                   PaymentIntervalsId = p.PaymentIntervalsId,
                   Intervals = p.PaymentIntervals.Intervals,
                   IntervalsDescription = p.PaymentIntervals.Description,
                   LawsId = p.LawsId,
                   Law1 = p.Laws.Law1,
                   LawDescription = p.Laws.Description
               });
            return source;
        }

        public static async Task<int> addUsers(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {

                    var usersObj = new DAL.Models.User();
                    JsonConvert.PopulateObject(values, usersObj);

                    var usersValid = db.Users.Where(m => m.UserName == usersObj.UserName).FirstOrDefault();
                    if (usersValid != null)
                    {
                        throw new UsersException("User Name already exists.");
                    }

                    usersObj.SendEmail = false;

                    db.Users.Add(usersObj);

                    var addressObj = new DAL.Models.Address();
                    JsonConvert.PopulateObject(values, addressObj);

                    var userDetailsObj = new DAL.Models.UserDetail();
                    JsonConvert.PopulateObject(values, userDetailsObj);
                    var UserDetailsValid = db.UserDetails.Where(v => v.Idnumber == userDetailsObj.Idnumber).FirstOrDefault();
                    if (UserDetailsValid != null)
                    {
                        throw new UsersException("User ID Number already exists.");
                    }

                    UserDetailsValid = db.UserDetails.Where(m => m.Email == userDetailsObj.Email).FirstOrDefault();
                    if (UserDetailsValid != null)
                    {
                        throw new UsersException("User Email already exists.");
                    }

                    userDetailsObj.HomeAddress = addressObj;
                    usersObj.UserDetails.Add(userDetailsObj);

                    db.UserDetails.Add(userDetailsObj);
                    await db.SaveChangesAsync();

                    dbContextTransaction.Commit();
                    return usersObj.Id;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    dbContextTransaction.Rollback();
                    throw new UsersException(ex.Message);
                }
            }


        }

        public static async Task<int> editUsers(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var UsersObj = db.Users.FirstOrDefault(d => d.Id == key);
            if (UsersObj == null) throw new UsersException("User does not exist.");

            JsonConvert.PopulateObject(values, UsersObj);

            var usersValid = db.Users.Where(m => m.UserName == UsersObj.UserName && m.Id != UsersObj.Id).FirstOrDefault();
            if (usersValid != null)
            {
                throw new UsersException("User Name already exists.");
            }

            var userDetailsObj = UsersObj.UserDetails.FirstOrDefault();
            if (userDetailsObj == null) throw new UsersException("User Details does not exist.");
            JsonConvert.PopulateObject(values, userDetailsObj);

            var UserDetailsValid = db.UserDetails.Where(v => v.Idnumber == userDetailsObj.Idnumber && v.Id != userDetailsObj.Id).FirstOrDefault();
            if (UserDetailsValid != null)
            {
                throw new UsersException("User ID Number already exists.");
            }
            UserDetailsValid = db.UserDetails.Where(m => m.Email == userDetailsObj.Email && m.Id != userDetailsObj.Id).FirstOrDefault();
            if (UserDetailsValid != null)
            {
                throw new UsersException("User Email already exists.");
            }

            JsonConvert.PopulateObject(values, userDetailsObj.HomeAddress);
            await db.SaveChangesAsync();

            return UsersObj.Id;
        }

        public static int deactivateUser(DAL.DTO.ActivateUser user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Users.Where(w => w.Id == user.Id).FirstOrDefault();

            if (Obj.DepartmentUsers.Count > 0)
            {
                throw new UsersException("The User is allocated to a Department. Allocation must be removed before the user can be disabled.");
            }

            Obj.IsDisabled = true;
            db.SaveChanges();

            return Obj.Id;
        }

        public static int activateUser(DAL.DTO.ActivateUser user)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Users.Where(w => w.Id == user.Id).FirstOrDefault();
            Obj.IsDisabled = false;
            db.SaveChanges();

            return Obj.Id;
        }

        public static IQueryable<DAL.DTO.User> getActiveUsers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Users.Where(r => !r.IsDisabled)
               .Select(p => new DAL.DTO.User
               {
                   Id = p.Id,
                   Name = p.Name,
                   Surname = p.Surname,
                   UserName = p.UserName
               });
            return source;
        }

        public static int sendEmail(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Users.Where(w => w.Id == id).FirstOrDefault();

            if (Obj.LastActivity < DateTime.Now.Date && Obj.SendEmail == false)
            {
                Obj.SendEmail = true;
                Obj.LastActivity = DateTime.Now;
            }

            db.SaveChanges();

            return Obj.Id;
        }

        public static int dontEmail(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Users.Where(w => w.Id == id).FirstOrDefault();

            Obj.SendEmail = false;

            db.SaveChanges();

            return Obj.Id;
        }
    }
}
