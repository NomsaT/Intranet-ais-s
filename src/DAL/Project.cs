using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Project
    {
        public static IQueryable<DAL.DTO.Project> getProjects()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Projects
               .Select(p => new DAL.DTO.Project
               {
                   Id = p.Id,
                   Name = p.Name,
                   StartDate = p.StartDate,
                   EndDate = p.EndDate,
                   ProjectStatusId = p.ProjectStatusId,
                   Budget = p.Budget
               });
            return source;
        }

        public static int addProjects(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Project();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.Projects.Where(m => m.Name == Obj.Name).FirstOrDefault();
            if (check != null)
            {
                throw new ProjectException("Project already exists.");
            }

            if(Obj.StartDate > Obj.EndDate)
            {
                throw new ProjectException("Project End date cannot be before the start date.");
            }

            Obj.ProjectStatusId = (int)DAL.Constants.ProjectStatus.OPEN;
            db.Projects.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editProjects(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Projects.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProjectException("Project does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Projects.Where(m => m.Name == Obj.Name && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new ProjectException("Project already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteProjects(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Projects.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProjectException("Project does not exist.");

            if (Obj.InternalOrders.Count > 0)
            {
                throw new ProjectException("The project is linked to an internal order, the project can be removed once the order is closed.");
            }

            db.Projects.Remove(Obj);
            await db.SaveChangesAsync();

            return "Project Deleted";
        }

        public static int closeProject(int id)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            DAL.DTO.Project project = new DAL.DTO.Project();

            var projObj = db.Projects.Where(i => i.Id == id).FirstOrDefault();

            projObj.ProjectStatusId = (int)DAL.Constants.ProjectStatus.CLOSE;

            db.SaveChanges();

            return projObj.Id;

        }
    }
}
