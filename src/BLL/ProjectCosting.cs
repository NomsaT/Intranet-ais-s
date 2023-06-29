using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProjectCosting
    {
        public static IQueryable<DAL.DTO.ProjectCosting> getProjectCosting()
        {
            return DAL.ProjectCosting.getProjectCosting();
        }
    }
}
