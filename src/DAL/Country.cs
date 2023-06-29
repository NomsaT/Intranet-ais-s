using System.Linq;

namespace DAL
{
    public static class Country
    {
        public static IQueryable<DAL.DTO.Country> getCountry()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Countries
               .Select(p => new DAL.DTO.Country
               {
                   Id = p.Id,
                   Name = p.Name
               });
            return source;
        }
    }
}
