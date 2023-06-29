using System.Linq;

namespace BLL
{
    public static class OrdersReport
    {
        public static IQueryable<DAL.DTO.InternalOrder> getOrdersReport()
        {
            return DAL.OrdersReport.getOrdersReport();
        }
    }
}
