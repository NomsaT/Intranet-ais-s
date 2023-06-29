using System.Linq;

namespace BLL
{
    public static class IncreaseType
    {
        public static IQueryable<DAL.DTO.IncreaseType> getIncreaseType()
        {
            return DAL.IncreaseType.getIncreaseType();
        }
    }
}
