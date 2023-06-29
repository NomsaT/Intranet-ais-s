using System.Linq;

namespace BLL
{
    public static class Race
    {
        public static IQueryable<DAL.DTO.Race> getRace()
        {
            return DAL.Race.getRace();
        }

    }
}
