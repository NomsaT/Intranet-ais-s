using System.Linq;

namespace DAL
{
    public static class AllocateUsers
    {
        public static IQueryable<DAL.DTO.AllocateUsers> getAllocateUsers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.UserDetails
               .Select(p => new DAL.DTO.AllocateUsers
               {
                   Id = p.User.Id,
                   Name = p.User.Name,
                   Surname = p.User.Surname,
                   UserName = p.User.UserName,
                   EmployeeNumber = p.EmployeeNumber,
                   IsDisabled = p.User.IsDisabled,
                   Percentage = p.User.DepartmentUsers.Sum(e => e.Percentage)
               });
            return source;
        }
    }
}
