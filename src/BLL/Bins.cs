using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Bins
    {
        public static IQueryable<DAL.DTO.Bins> getBins()
        {
            return DAL.Bins.getBins();
        }
        public static int addBins(string values)
        {
            return DAL.Bins.addBins(values);
        }

        public static Task<int> editBins(int key, string values)
        {
            return DAL.Bins.editBins(key, values);
        }

        public static Task<int> deleteBins(int key)
        {
            return DAL.Bins.deleteBins(key);
        }
    }
}
