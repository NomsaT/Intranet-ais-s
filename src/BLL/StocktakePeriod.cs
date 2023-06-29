using DAL.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class StocktakePeriod
    {
        public static int AddStocktakePeriod(string values)
        {
            return DAL.StocktakePeriod.AddStocktakePeriod(values);
        }

        public static Task<object> EditStocktakePeriodAsync(int key, string values)
        {
            return DAL.StocktakePeriod.EditStocktakePeriodsync(key, values);
        }

        public static Task<object> DeleteStocktakePeriodAsync(int key)
        {
            return DAL.StocktakePeriod.DeleteStocktakePeriodAsync(key);
        }
        public static IQueryable<DAL.DTO.StocktakeCycle> GetStocktakeCycle()
        {
            return DAL.Stocktake.GetStocktakeCycle();
        }
    }
}