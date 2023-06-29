using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Stocktake
    {
        public static IQueryable<DAL.DTO.Stocktake> GetStocktake()
        {
            return DAL.Stocktake.GetStocktake();
        }

        public async static Task<Boolean> Stocktaking(List<DAL.DTO.Stocktake> list)
        {
            return await DAL.Stocktake.Stocktaking(list);
        }

        public async static Task<DAL.DTO.Stocktake> ApproveStocktake(int id)
        {
            return await DAL.Stocktake.ApproveStocktake(id);
        }

        public async static Task<DAL.DTO.Stocktake> RecountStocktake(int id, int userId)
        {
            return await DAL.Stocktake.RecountStocktake(id, userId);
        }

        public async static Task<List<DAL.DTO.StocktakeLog>> GetStocktakeLog()
        {
            return await DAL.Stocktake.GetStocktakeLog();
        }

        public async static Task<List<DAL.DTO.Stocktake>> GetRecounts(int userId)
        {
            return await DAL.Stocktake.GetRecounts(userId);
        }
    }
}
