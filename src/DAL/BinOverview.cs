using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class BinOverview
    {

        public static IQueryable<DAL.DTO.Bins> getBinOverview()
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Bins
                .Select(s => new DAL.DTO.Bins
                {
                    Id = s.Id,
                    Name = s.Name,
                    StoreId = s.StoreId,
                    StoreName = s.Store.Name
                });
            return source;
        }
    }
}
