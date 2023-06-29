using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class StockCorrection
    {
        public static IQueryable<DAL.DTO.StockQuantity> getStockCorrection()
        {
            return DAL.StockCorrection.getStockCorrection();
        }
        public static Task<int> editProfitCenter(int key, string values)
        {
            return DAL.StockCorrection.editStockCorrection(key, values);
        }

    }
}
