using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class ShelfLifeType
    {
        public static IQueryable<DAL.DTO.ShelfLifeType> getShelfLifeType()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ShelfLifeTypes
               .Select(p => new DAL.DTO.ShelfLifeType
               {
                   Id = p.Id,
                   Type = p.Type,
                   Days = p.Days
               });
            return source;
        }
    }
}
