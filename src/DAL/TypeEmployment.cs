using System.Linq;

namespace DAL
{
    public static class TypeEmployment
    {
        public static IQueryable<DAL.DTO.TypeEmployment> getTypeEmployment()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.TypeEmployments
               .Select(p => new DAL.DTO.TypeEmployment
               {
                   Id = p.Id,
                   Type = p.Type

               });
            return source;
        }
    }
}
