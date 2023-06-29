using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProductionStock
    {
        public static List<DAL.DTO.ProductionStock> getProductionStock()
        {
            return DAL.ProductionStock.getProductionStock();
        }
    }
}
