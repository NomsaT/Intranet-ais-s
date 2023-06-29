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
    public class ProductItemController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.ProductItem.getProductItem(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetTotalProductItem")]
        public async Task<IActionResult> GetTotalProductItem(int productId)
        {
            try
            {
                return Ok(BLL.ProductItem.GetTotalProductItem(productId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("RemoveAllProductItem")]
        public async Task<IActionResult> RemoveAllProductItem(int productId)
        {
            try
            {
                return Ok(BLL.ProductItem.RemoveAllProductItem(productId));
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
                return Ok(BLL.ProductItem.addProductItem(values));
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
                return Ok(await BLL.ProductItem.editProductItem(key, values));
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
                return Ok(await BLL.ProductItem.deleteProductItem(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
