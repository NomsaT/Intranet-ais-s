using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Department
    {

        public static IQueryable<DAL.DTO.Department> getDepartmentsMain()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments
               .Select(p => new DAL.DTO.Department
               {
                   Id = p.Id,
                   Name = p.Name,
                   Description = p.Description,
                   Abbreviation = p.Abbreviation,
                   AbbAndName = p.Abbreviation + " - " + p.Name,
                   Color = p.Color,
                   CostTypeId = p.CostTypeId,
                   GeneralPurchasing = p.GeneralPurchasing
               });
            return source;
        }

        public static IQueryable<DAL.DTO.Department> getDepartmentr()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments
               .Select(p => new DAL.DTO.Department
               {
                   Id = p.Id,
                   Name = (p.GeneralPurchasing == true) ? p.Name + " (" + p.CostType.Abbreviation + ")" + " (Default Department)" : p.Name + " ("+p.CostType.Abbreviation+")",
                   Description = p.Description,
                   Abbreviation = p.Abbreviation,
                   AbbAndName = p.Abbreviation + " - " + p.Name,
                   Color = p.Color,
                   CostTypeId = p.CostTypeId,
                   GeneralPurchasing = p.GeneralPurchasing
               });
            return source;
        }

        public static IQueryable<DAL.DTO.Department> filterDepartment(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var departments = db.StockQuantities.Where(t => t.StockId == id).Select(d => d.Department).Distinct()
            .Select(p => new DAL.DTO.Department
            {
                Id = p.Id,                
                Name = (p.GeneralPurchasing == true) ? p.Name + " (" + p.CostType.Abbreviation + ")" + " (Default Department)" : p.Name + " (" + p.CostType.Abbreviation + ")" ,
                Description = p.Description,
                Abbreviation = p.Abbreviation,
                AbbAndName = p.Abbreviation + " - " + p.Name,
                Color = p.Color,
                CostTypeId = p.CostTypeId,
                GeneralPurchasing = p.GeneralPurchasing                
            });

            return departments;
        }

        public static IQueryable<DAL.DTO.Department> getDepartmentStock(int stockId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.StockQuantities.Where(w => w.StockId == stockId)
                .Select(p => new DAL.DTO.Department
                {
                    Id = p.Department.Id,
                    Name = p.Department.Name + " (" + p.Department.CostType.Abbreviation + ")",
                    Description = p.Department.Description,
                    Color = p.Department.Color,
                    Abbreviation = p.Department.Abbreviation,
                    CostTypeId = p.Department.CostTypeId
                }).Distinct();
            return source;
        }

        public static IQueryable<string> getDepartmentNames()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments
               .Select(p => p.Name + " (" + p.CostType.Abbreviation + ")").Distinct();
            return source;
        }

        public static IQueryable<DAL.DTO.Department> getDepartmentFilter(int profitCenterId)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments
               .Select(p => new DAL.DTO.Department
               {
                   Id = p.Id,
                   Name = p.Name + " (" + p.CostType.Abbreviation + ")",
                   Description = p.Description,
                   Abbreviation = p.Abbreviation,
                   Color = p.Color,
                   CostTypeId = p.CostTypeId,
                   GeneralPurchasing = p.GeneralPurchasing
               }).Distinct();
            return source;
        }

        public static int addDepartment(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Department();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.Departments.Where(m => m.Name == Obj.Name && m.CostTypeId == Obj.CostTypeId).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentException("Department already exists.");
            }

            if (!string.IsNullOrEmpty(Obj.Abbreviation))
            {
                var Abbreviation = db.Departments.Where(m => m.Abbreviation == Obj.Abbreviation).FirstOrDefault();
                if (Abbreviation != null)
                {
                    throw new DepartmentException("The Abbreviation already exists.");
                }
            }

            if(Obj.GeneralPurchasing == true)
            {
                var defaultDep = db.Departments.Where(d => d.GeneralPurchasing == true).FirstOrDefault();
                if (defaultDep != null)
                {
                    throw new ProfitCenterException("The default department is already set. There can only be one default department.");
                }
            }
            else
            {
                Obj.GeneralPurchasing = false;
            }         

            db.Departments.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editDepartment(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Departments.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentException("Department does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Departments.Where(m => m.Name == Obj.Name && m.CostTypeId == Obj.CostTypeId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new DepartmentException("Department already exists.");
            }

            if (!string.IsNullOrEmpty(Obj.Abbreviation))
            {
                var Abbreviation = db.Departments.Where(m => m.Abbreviation == Obj.Abbreviation && m.Id != Obj.Id).FirstOrDefault();
                if (Abbreviation != null)
                {
                    throw new DepartmentException("The Abbreviation already exists.");
                }
            }

            if (Obj.GeneralPurchasing == true)
            {
                var defaultDep = db.Departments.Where(d => d.GeneralPurchasing == true && d.Id != Obj.Id).FirstOrDefault();
                if (defaultDep != null)
                {
                    throw new ProfitCenterException("The default department is already set. There can only be one default department.");
                }
            }
            else
            {
                Obj.GeneralPurchasing = false;
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteDepartment(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Departments.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new DepartmentException("Department does not exist.");

            if (Obj.DepartmentManagers.Count > 0)
            {
                throw new DepartmentException("The Department is linked to a Department Manager, remove the Department Manager from the department before deleting the current department.");
            }

            if (Obj.StockQuantities.Count > 0)
            {
                throw new DepartmentException("The Department is assigned to stock.");
            }

            if (Obj.DepartmentUsers.Count > 0)
            {
                throw new DepartmentException("The Department has users allocated to it.");
            }

            if (Obj.Stocks.Count > 0)
            {
                throw new DepartmentException("The Department is linked to a stock item.");
            }

            db.Departments.Remove(Obj);
            await db.SaveChangesAsync();

            return "Department Deleted";
        }

        public static int getDefaultDepartment()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Departments.Where(d => d.GeneralPurchasing == true).FirstOrDefault().Id;
            return source;
        }
    }
}
