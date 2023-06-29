using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Quarantine
    {        
        public static int ReturnToSupplier(List<DAL.DTO.Stock> data)
        {
            DAL.Quarantine.ReturnToSupplier(data);
            return 1;
        }

        public static int LogReturnToSupplier(List<DAL.DTO.Stock> data)
        {
            DAL.Quarantine.LogReturnToSupplier(data);
            return 1;
        }
    }
}
