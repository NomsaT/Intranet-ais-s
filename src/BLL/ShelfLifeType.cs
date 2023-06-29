using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ShelfLifeType
    {
        public static IQueryable<DAL.DTO.ShelfLifeType> getShelfLifeType()
        {
            return DAL.ShelfLifeType.getShelfLifeType();
        }
    }
}
