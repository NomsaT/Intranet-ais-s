using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class PriceLookUp
    {
        public static IQueryable<DAL.DTO.PriceLookup> getPriceLookUp()
        {
            return DAL.PriceLookUp.getPriceLookUp();
        }

        public static int addPriceLookUp(string values)
        {
            return DAL.PriceLookUp.addPriceLookUp(values);
        }
        public static Task<int> editPriceLookUp(int key, string values)
        {
            return DAL.PriceLookUp.editPriceLookUp(key, values);
        }

        public static Task<string> deletePriceLookUp(int key, string values)
        {
            return DAL.PriceLookUp.deletePriceLookUp(key, values);
        }

        public static int editManualUpdate(DAL.DTO.PriceIncrease increase)
        {
            var data = DAL.PriceLookUp.editManualUpdate(increase);
            DAL.PriceLookUp.checkMonitoredStatus();
            return data;
        }
    }
}
