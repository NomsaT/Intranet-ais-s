using System.Linq;

namespace BLL
{
    public static class OnceOffReport
    {
        public static IQueryable<DAL.DTO.OnceOffItem> getOnceOffReport()
        {
            return DAL.OnceOffReport.getOnceOffReport();
        }
    }
}
