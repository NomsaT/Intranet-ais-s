using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Stock.getStock(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getStockWithQty")]
        public async Task<IActionResult> getStockWithQty(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Stock.getStockWithQty(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetStockData")]
        public async Task<IActionResult> GetStockData(int id, int action)
        {
            try
            {
                return Ok(BLL.Stock.GetStockData(id, action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateStock")]
        public async Task<object> AddUpdateStock(DAL.DTO.Stock stockItem)
        {
            try
            {
                return Ok(BLL.Stock.AddUpdateStock(stockItem));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getUnassignedStock")]
        public async Task<IActionResult> getUnassignedStock(DataSourceLoadOptions loadOptions, int StockId)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Stock.getUnassignedStock(StockId), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] int key)
        {
            try
            {
                return Ok(await BLL.Stock.deleteStock(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetTotalStockDep")]
        public async Task<IActionResult> GetTotalStockDep(int id,int stockId)
        {
            try
            {
                return Ok(BLL.Stock.GetTotalStockDep(id,stockId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetTotalStockLocation")]
        public async Task<IActionResult> GetTotalStockLocation(int id, int stockId)
        {
            try
            {
                return Ok(BLL.Stock.GetTotalStockLocation(id,stockId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetTotalStockStore")]
        public async Task<IActionResult> GetTotalStockStore(int locationId, int storeId, int stockId)
        {
            try
            {
                return Ok(BLL.Stock.GetTotalStockStore(locationId, storeId, stockId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetStockByName")]
        public async Task<IActionResult> GetStockByName(string name)
        {
            try
            {
                return Ok(BLL.Stock.GetStockByName(name));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetStockCount")]
        public async Task<IActionResult> GetStockCount(string code, int locId, int storeId)
        {
            try
            {
                return Ok(BLL.Stock.GetStockCount(code, locId, storeId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
