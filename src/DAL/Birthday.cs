using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Birthday
    {
        public static IQueryable<DAL.DTO.Birthday> getBirthdayList()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.UserDetails
               .Select(p => new DAL.DTO.Birthday
               {
                   Birthdays = p.Birthday,
                   FullNameAndSurname = p.User.Name + " " + p.User.Surname,
                   isDisabled = p.User.IsDisabled
               }).Where(s => s.isDisabled == false)
               .OrderBy(r => r.Birthdays.Day)
               .ThenBy(d => d.Birthdays.Month);
            return source;
        }
    }
}
