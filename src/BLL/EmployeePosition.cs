using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class EmployeePosition
    {
        public static IQueryable<DAL.DTO.EmployeePosition> getEmployeePosition()
        {
            return DAL.EmployeePosition.getEmployeePosition();
        }

        public static int addEmployeePosition(string values)
        {
            return DAL.EmployeePosition.addEmployeePosition(values);
        }
        public static Task<int> editEmployeePosition(int key, string values)
        {
            return DAL.EmployeePosition.editEmployeePosition(key, values);
        }

        public static Task<string> deleteEmployeePosition(int key)
        {
            return DAL.EmployeePosition.deleteEmployeePosition(key);
        }
    }
}
