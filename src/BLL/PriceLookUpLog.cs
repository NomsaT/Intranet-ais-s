using System.Linq;

namespace BLL
{
    public static class PriceLookUpLog
    {
        public static IQueryable<DAL.DTO.PriceLookupLog> getPriceLookUpLog()
        {
            return DAL.PriceLookUpLog.getPriceLookUpLog();
        }
    }
}
