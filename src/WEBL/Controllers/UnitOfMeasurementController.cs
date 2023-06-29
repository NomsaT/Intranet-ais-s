using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasurementController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.UnitOfMeasurement.getUnitOfMeasurement(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getUOMbyStock")]
        public object getUOMbyStock(int stockId)
        {
            try
            {
                return BLL.UnitOfMeasurement.getUOMbyStock(stockId);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getUOMbyStockConsume")]
        public object getUOMbyStockConsume(int itemStockId)
        {
            try
            {
                return BLL.UnitOfMeasurement.getUOMbyStockConsume(itemStockId);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        
        /* [Authorize]*/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string values)
        {
            try
            {
                return Ok(BLL.UnitOfMeasurement.addUnitOfMeasurement(values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] int key, [FromForm] string values)
        {
            try
            {
                return Ok(await BLL.UnitOfMeasurement.editUnitOfMeasurement(key, values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] int key)
        {
            try
            {
                return Ok(await BLL.UnitOfMeasurement.deleteUnitOfMeasurement(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
