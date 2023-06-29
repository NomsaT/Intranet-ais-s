using System.Linq;

namespace BLL
{
    public static class Country
    {
        public static IQueryable<DAL.DTO.Country> getCountry()
        {
            return DAL.Country.getCountry();
        }
    }
}
