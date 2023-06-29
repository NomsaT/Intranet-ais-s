using System.Linq;

namespace BLL
{
    public static class FullEmployeeName
    {
        public static IQueryable<DAL.DTO.FullEmployeeName> getFullEmployeeName()
        {
            return DAL.FullEmployeeName.getFullEmployeeName();
        }

        public static object getAllMangers()
        {
            return DAL.FullEmployeeName.getAllMangers();
        }
        public static IQueryable<DAL.DTO.FullEmployeeNameTitle> getFullEmployeeNameTitle()
        {
            return DAL.FullEmployeeName.getFullEmployeeNameTitle();
        }

        public static IQueryable<DAL.DTO.DepartmentApprovers> getDepartmentManagersFullName(int departmentId)
        {
            return DAL.FullEmployeeName.getDepartmentManagersFullName(departmentId);
        }

        public static IQueryable<DAL.DTO.DepartmentApprovers> getFilteredDepartmentManagers()
        {
            return DAL.FullEmployeeName.getFilteredDepartmentManagers();
        }        
    }
}
