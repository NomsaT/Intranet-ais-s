using DAL.DTO;
using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocktakeController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Stocktake.GetStocktake(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpPost("Stocktaking")]
        public async Task<IActionResult> Stocktaking(List<DAL.DTO.Stocktake> list)
        {
            try
            {
                return Ok(await BLL.Stocktake.Stocktaking(list));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("ApproveStocktake")]
        public async Task<IActionResult> ApproveStocktake(int id)
        {
            try
            {
                return Ok(await BLL.Stocktake.ApproveStocktake(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("RecountStocktake")]
        public async Task<IActionResult> RecountStocktake(int id, int userId)
        {
            try
            {
                return Ok(await BLL.Stocktake.RecountStocktake(id, userId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        
        [HttpGet("GetStocktakeLog")]
        public async Task<IActionResult> GetStocktakeLog()
        {
            try
            {
                return Ok(await BLL.Stocktake.GetStocktakeLog());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetRecounts")]
        public async Task<IActionResult> GetRecounts(int userId)
        {
            try
            {
                return Ok(await BLL.Stocktake.GetRecounts(userId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
