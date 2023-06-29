using System.Linq;

namespace DAL
{
    public static class Laws
    {
        public static IQueryable<DAL.DTO.Law> getLaws()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Laws
               .Select(p => new DAL.DTO.Law
               {
                   Id = p.Id,
                   Law1 = p.Law1,
                   Description = p.Description

               });
            return source;
        }
    }
}
