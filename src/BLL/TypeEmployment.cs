using System.Linq;

namespace BLL
{
    public static class TypeEmployment
    {
        public static IQueryable<DAL.DTO.TypeEmployment> getTypeEmployment()
        {
            return DAL.TypeEmployment.getTypeEmployment();
        }
    }
}
