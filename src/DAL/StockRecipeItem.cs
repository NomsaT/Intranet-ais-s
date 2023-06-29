using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class StockRecipeItem
    {
        public static IQueryable<DAL.DTO.Recipe> getStockRecipeItem()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Recipes
               .Select(p => new DAL.DTO.Recipe
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   StockComponentId = p.StockComponentId,
                   Quantity = p.Quantity,
                   UomName = p.Stock.Uom.Name

               });
            return source;
        }

    }
}
