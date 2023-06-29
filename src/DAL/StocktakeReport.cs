using DAL.DTO;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class StocktakeReport
    {
        public async static Task<List<DTO.StocktakeReport>> GetStocktakeCycleReport()
        {
            using (var db = new DAL.Models.AISContext())
            {

                var reports = await db.StocktakeReports.Select(x => new DTO.StocktakeReport
                {
                    Id = x.Id,
                    StocktakeCycleId = x.StocktakeCycleId,
                    ClosingQty = x.ClosingQty,
                    OpeningQty = x.OpeningQty,
                    PlantLocationName = x.PlantLocationName,
                    StockFullName = x.StockFullName,
                    StoreName = x.StoreName,
                    Discrepancy = x.ClosingQty - x.OpeningQty
                }).ToListAsync();
                return reports;
            }
        }
    }
}