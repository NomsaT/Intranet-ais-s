using System.Linq;

namespace BLL
{
    public static class ServiceReport
    {
        public static IQueryable<DAL.DTO.Service> getServiceReport()
        {
            return DAL.ServiceReport.getServiceReport();
        }
    }
}
