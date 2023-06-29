using System.Linq;

namespace BLL
{
    public static class AllocateUsers
    {
        public static IQueryable<DAL.DTO.AllocateUsers> getAllocateUsers()
        {
            return DAL.AllocateUsers.getAllocateUsers();
        }
    }
}
