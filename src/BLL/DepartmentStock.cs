using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class DepartmentStock
    {
        public static IQueryable<DAL.DTO.DepartmentStock> getDepartmentStock()
        {
            return DAL.DepartmentStock.getDepartmentStock();
        }

        public static Task<int> editDepartmentStock(int key, string values)
        {
            return DAL.DepartmentStock.editDepartmentStock(key, values);
        }
    }
}
