using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StockRecipe
    {
        public static IQueryable<DAL.DTO.Stock> getStockRecipe()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Stocks
               .Where(s => s.StockCategoryId == (int)DAL.Constants.StockCategory.RECIPE )
               .Select(p => new DAL.DTO.Stock
               {
                   Id = p.Id,
                   SupplierId = p.SupplierId,
                   InternalProductName = p.InternalProductName,
                   Code = p.Code
               });
            return source;
        }

    }
}
