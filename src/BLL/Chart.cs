using System.Linq;
using System.Threading.Tasks;
namespace BLL
{
    public static class Chart
    {
        
        public static object getOrders()
        {
            return DAL.Chart.getOrders();
        }
        public static object getDepartmentStock()
        {
            return DAL.Chart.getDepartmentStock();
        }
    }
}
