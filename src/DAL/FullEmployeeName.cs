using System.Linq;

namespace DAL
{
    public static class FullEmployeeName
    {
        public static IQueryable<DAL.DTO.FullEmployeeName> getFullEmployeeName()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Users
               .Select(p => new DAL.DTO.FullEmployeeName
               {
                   Id = p.Id,
                   FullName = p.Name + " " + p.Surname,
                   IsDisabled = p.IsDisabled,
                   FullNamePosition = p.Name + " " + p.Surname + " - " + p.UserDetails.First().EmployeePosition.Position
               }).Where(x => !x.IsDisabled);
            return source;
        }

        public static IQueryable<DAL.DTO.FullEmployeeNameTitle> getFullEmployeeNameTitle()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentManagers
               .Select(p => new DAL.DTO.FullEmployeeNameTitle
               {
                   Id = p.Id,
                   FullNameTitle = p.User.Name + " " + p.User.Surname + " - " + p.User.UserDetails.First().EmployeePosition.Position

               });
            return source;
        }

        public static object getAllMangers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentManagers
               .Select(p => new DAL.DTO.DepartmentApprovers
               {
                   ManagerFullName = p.User.Name + " " + p.User.Surname
               }).ToList();
            return source;
        }

        public static IQueryable<DAL.DTO.DepartmentApprovers> getDepartmentManagersFullName(int departmentId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentManagers
               .Select(p => new DAL.DTO.DepartmentApprovers
               {
                   Id = p.Id,
                   DepartmentId = p.DepartmentId,
                   ManagerFullName = p.User.Name + " " + p.User.Surname

               }).Where(s => s.DepartmentId == departmentId);
            return source;
        }

        public static IQueryable<DAL.DTO.DepartmentApprovers> getFilteredDepartmentManagers()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.DepartmentManagers
               .Select(p => new DAL.DTO.DepartmentApprovers
               {
                   Id = p.Id,
                   DepartmentId = p.DepartmentId,
                   ManagerFullName = p.User.Name + " " + p.User.Surname

               }).Distinct();
            return source;
        }

    }
}
