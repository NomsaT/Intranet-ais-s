using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class VatStored
    {
        public static IQueryable<DAL.DTO.VatStored> getVatStored()
        {
            return DAL.VatStored.getVatStored();
        }
        public static IQueryable<DAL.DTO.Glcode> getGLCodes()
        {
            return DAL.VatStored.getGLCodes();
        }
    }
}
