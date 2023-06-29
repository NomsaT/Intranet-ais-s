using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class QuarantineStock
    {
        public static IQueryable<DAL.DTO.DepartmentStock> getQuarantineStock()
        {
            return DAL.QuarantineStock.getQuarantineStock();
        }

        public static Task<int> editQuarantineStock(int key, string values)
        {
            return DAL.QuarantineStock.editQuarantineStock(key, values);
        }
    }
}
