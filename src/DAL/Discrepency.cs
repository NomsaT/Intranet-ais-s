using System.Linq;

namespace DAL
{
    public static class Discrepency
    {
        public static IQueryable<DAL.DTO.Discrepency> getDiscrepency()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var source = db.Discrepencies
               .Select(p => new DAL.DTO.Discrepency
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description
               });
            return source;
        }

    }
}
