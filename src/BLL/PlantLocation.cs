using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class PlantLocation
    {
        public static IQueryable<DAL.DTO.PlantLocation> getPlantLocation()
        {
            return DAL.PlantLocation.getPlantLocation();
        }

        public static IQueryable<DAL.DTO.PlantLocation> filterLocations(int id)
        {
            return DAL.PlantLocation.filterLocations(id);
        }

        public static object getDefaultStore(int id)
        {
            return DAL.PlantLocation.getDefaultStore(id);
        }

        public static int addPlantLocation(string values)
        {
            return DAL.PlantLocation.addPlantLocation(values);
        }
        public static Task<int> editPlantLocation(int key, string values)
        {
            return DAL.PlantLocation.editPlantLocation(key, values);
        }

        public static Task<string> deletePlantLocation(int key)
        {
            return DAL.PlantLocation.deletePlantLocation(key);
        }
    }
}
