using System.Linq;

namespace DAL
{
    public static class MaritalStatus
    {
        public static IQueryable<DAL.DTO.MaritalStatus> getMaritalStatus()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.MaritalStatuses
               .Select(p => new DAL.DTO.MaritalStatus
               {
                   Id = p.Id,
                   Status = p.Status

               });
            return source;
        }
    }
}
