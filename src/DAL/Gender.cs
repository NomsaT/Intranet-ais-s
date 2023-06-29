using System.Linq;

namespace DAL
{
    public static class Gender
    {
        public static IQueryable<DAL.DTO.Gender> getGender()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Genders
               .Select(p => new DAL.DTO.Gender
               {
                   Id = p.Id,
                   Gender1 = p.Gender1

               });
            return source;
        }
    }
}
