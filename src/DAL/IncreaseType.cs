using System.Linq;

namespace DAL
{
    public static class IncreaseType
    {
        public static IQueryable<DAL.DTO.IncreaseType> getIncreaseType()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.IncreaseTypes
               .Select(p => new DAL.DTO.IncreaseType
               {
                   Id = p.Id,
                   Type = p.Type

               });
            return source;
        }
    }
}
