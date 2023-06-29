using System.Linq;

namespace BLL
{
    public static class StockReport
    {
        public static IQueryable<DAL.DTO.StockLog> getStockReport()
        {
            return DAL.StockReport.getStockReport();
        }
    }
}
