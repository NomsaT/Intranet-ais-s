using System.Linq;

namespace DAL
{
    public static class Race
    {
        public static IQueryable<DAL.DTO.Race> getRace()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Races
               .Select(p => new DAL.DTO.Race
               {
                   Id = p.Id,
                   Race1 = p.Race1

               });
            return source;
        }
    }
}
