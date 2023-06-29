using System.Linq;

namespace BLL
{
    public static class GroupingStock
    {
        public static IQueryable<DAL.DTO.GroupingStock> getGroupingStock()
        {
            return DAL.GroupingStock.getGroupingStock();
        }
    }
}
