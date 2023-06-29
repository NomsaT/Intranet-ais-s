using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StockCategory
    {
        public static IQueryable<DAL.DTO.StockCategory> getStockCategory()
        {
            return DAL.StockCategory.getStockCategory();
        }

        public static int addStockCategory(string values)
        {
            return DAL.StockCategory.addStockCategory(values);
        }
        public static Task<int> editStockCategory(int key, string values)
        {
            return DAL.StockCategory.editStockCategory(key, values);
        }

        public static Task<string> deleteStockCategory(int key)
        {
            return DAL.StockCategory.deleteStockCategory(key);
        }
    }
}
