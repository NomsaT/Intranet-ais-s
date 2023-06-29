using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public class StocktakeReport
    {
        public async static Task<List<DAL.DTO.StocktakeReport>> GetStocktakeReports()
        {
            return await DAL.StocktakeReport.GetStocktakeCycleReport();
        }
    }
}