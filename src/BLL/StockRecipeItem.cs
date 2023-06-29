using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StockRecipeItem
    {
        public static IQueryable<DAL.DTO.Recipe> getStockRecipeItem()
        {
            return DAL.StockRecipeItem.getStockRecipeItem();
        }

    }
}
