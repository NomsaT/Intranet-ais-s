using System.Linq;

namespace BLL
{
    public static class Gender
    {
        public static IQueryable<DAL.DTO.Gender> getGender()
        {
            return DAL.Gender.getGender();
        }
    }
}
