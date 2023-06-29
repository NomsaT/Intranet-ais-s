using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Department
    {
        public static IQueryable<DAL.DTO.Department> getDepartmentMain()
        {
            return DAL.Department.getDepartmentsMain();
        }
        public static IQueryable<DAL.DTO.Department> getDepartment()
        {
            return DAL.Department.getDepartmentr();
        }

        public static IQueryable<DAL.DTO.Department> filterDepartment(int id)
        {
            return DAL.Department.filterDepartment(id);
        }

        public static IQueryable<DAL.DTO.Department> getDepartmentStock(int stockId)
        {
            return DAL.Department.getDepartmentStock(stockId);
        }

        public static IQueryable<string> getDepartmentNames()
        {
            return DAL.Department.getDepartmentNames();
        }

        public static IQueryable<DAL.DTO.Department> getDepartmentFilter(int profitCenterId)
        {
            return DAL.Department.getDepartmentFilter(profitCenterId);
        }

        public static int addDepartment(string values)
        {
            return DAL.Department.addDepartment(values);
        }
        public static Task<int> editDepartment(int key, string values)
        {
            return DAL.Department.editDepartment(key, values);
        }

        public static Task<string> deleteDepartment(int key)
        {
            return DAL.Department.deleteDepartment(key);
        }

        public static object getDefaultDepartment()
        {
            return DAL.Department.getDefaultDepartment();
        }
    }
}
