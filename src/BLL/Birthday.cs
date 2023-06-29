using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Birthday
    {
        public static IQueryable<DAL.DTO.Birthday> getBirthdayList()
        {
            return DAL.Birthday.getBirthdayList();
        }
    }
}
