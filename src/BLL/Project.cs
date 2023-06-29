using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Project
    {
        public static IQueryable<DAL.DTO.Project> getProjects()
        {
            return DAL.Project.getProjects();
        }

        public static int addProjects(string values)
        {
            return DAL.Project.addProjects(values);
        }
        public static Task<int> editProjects(int key, string values)
        {
            return DAL.Project.editProjects(key, values);
        }

        public static Task<string> deleteProjects(int key)
        {
            return DAL.Project.deleteProjects(key);
        }

        public static int closeProject(int id)
        {
            return DAL.Project.closeProject(id);
        }
    }
}
