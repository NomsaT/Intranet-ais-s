using System.Linq;

namespace BLL
{
    public static class Discrepency
    {
        public static IQueryable<DAL.DTO.Discrepency> getDiscrepency()
        {
            return DAL.Discrepency.getDiscrepency();
        }

    }
}
