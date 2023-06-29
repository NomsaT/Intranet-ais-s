using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class CostTypes
    {
        public static IQueryable<DAL.DTO.CostType> getCostTypes()
        {
            return DAL.CostTypes.getCostTypes();
        }

        public static int addCostTypes(string values)
        {
            return DAL.CostTypes.addCostTypes(values);
        }
        public static Task<int> editCostTypes(int key, string values)
        {
            return DAL.CostTypes.editCostTypes(key, values);
        }

        public static Task<string> deleteCostTypes(int key)
        {
            return DAL.CostTypes.deleteCostTypes(key);
        }
    }
}
