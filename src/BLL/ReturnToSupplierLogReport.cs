using System.Linq;

namespace BLL
{
    public static class ReturnToSupplierLogReport
    {
        public static IQueryable<DAL.DTO.ReturnToSupplier> getReturnToSupplierLogReport()
        {
            return DAL.ReturnToSupplierLogReport.getReturnToSupplierLogReport();
        }
    }
}
