using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StockGroup
    {
        public static IQueryable<DAL.DTO.StockGroup> getStockGroup()
        {
            return DAL.StockGroup.getStockGroup();
        }

        public static int addStockGroup(string values)
        {
            return DAL.StockGroup.addStockGroup(values);
        }
        public static Task<int> editStockGroup(int key, string values)
        {
            return DAL.StockGroup.editStockGroup(key, values);
        }

        public static Task<string> deleteStockGroup(int key)
        {
            return DAL.StockGroup.deleteStockGroup(key);
        }
    }
}
