using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ReturnToSupplier
    {
        public static IQueryable<DAL.DTO.ReturnToSupplier> getReturnToSupplier()
        {
            return DAL.ReturnToSupplier.getReturnToSupplier();
        }
    }
}
