using System.Linq;

namespace BLL
{
    public static class MaritalStatus
    {
        public static IQueryable<DAL.DTO.MaritalStatus> getMaritalStatus()
        {
            return DAL.MaritalStatus.getMaritalStatus();
        }
    }
}
