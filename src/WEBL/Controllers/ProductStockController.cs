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
    public class ProductStockController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.ProductStock.getProductStock(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetTotalProductStock")]
        public async Task<IActionResult> GetTotalProductStock(int productId)
        {
            try
            {
                return Ok(BLL.ProductStock.GetTotalProductStock(productId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("RemoveAllProductStock")]
        public async Task<IActionResult> RemoveAllProductStock(int productId)
        {
            try
            {
                return Ok(BLL.ProductStock.RemoveAllProductStock(productId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetProductItemsByBarcode")]
        public async Task<IActionResult> GetProductItemsByBarcode(int barcode, int productionStoreId)
        {
            try
            {
                return Ok(BLL.ProductStock.getProductItemsByBarcode(barcode, productionStoreId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        //[HttpGet("ConsumeProductStock")]
        //public async Task<IActionResult> ConsumeProductStock(List<int> productStockIds, List<int> productItemIds)
        //{
        //    try
        //    {
        //        return Ok(await BLL.ProductStock.ConsumeProductStock(productStockIds, productItemIds));
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error(e);
        //        return BadRequest(ErrorMessage.GetMessage(e));
        //    }
        //}

        /* [Authorize]*/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string values)
        {
            try
            {
                return Ok(BLL.ProductStock.addProductStock(values));
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
                return Ok(await BLL.ProductStock.editProductStock(key, values));
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
                return Ok(await BLL.ProductStock.deleteProductStock(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
