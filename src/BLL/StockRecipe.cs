using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StockRecipe
    {
        public static IQueryable<DAL.DTO.Stock> getStockRecipe()
        {
            return DAL.StockRecipe.getStockRecipe();
        }

    }
}
