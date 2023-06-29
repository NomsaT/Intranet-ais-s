using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class UnitOfMeasurement
    {
        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUnitOfMeasurement()
        {
            return DAL.UnitOfMeasurement.getUnitOfMeasurement();
        }

        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUOMbyStock(int stockId)
        {
            return DAL.UnitOfMeasurement.getUOMbyStock(stockId);
        }

        public static IQueryable<DAL.DTO.UnitOfMeasurement> getUOMbyStockConsume(int itemStockId)
        {
            return DAL.UnitOfMeasurement.getUOMbyStockConsume(itemStockId);
        }
        
        public static int addUnitOfMeasurement(string values)
        {
            return DAL.UnitOfMeasurement.addUnitOfMeasurement(values);
        }
        public static Task<int> editUnitOfMeasurement(int key, string values)
        {
            return DAL.UnitOfMeasurement.editUnitOfMeasurement(key, values);
        }

        public static Task<string> deleteUnitOfMeasurement(int key)
        {
            return DAL.UnitOfMeasurement.deleteUnitOfMeasurement(key);
        }
    }
}
