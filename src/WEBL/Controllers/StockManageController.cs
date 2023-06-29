using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockManageController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /* [Authorize]*/
        [HttpPost("AddStock")]
        public object AddStock(DAL.DTO.StockQuantity stock)
        {
            try
            {
                return Ok(BLL.StockManage.addStock(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("RemoveStock")]
        public object RemoveStock(DAL.DTO.StockQuantity stock)
        {
            try
            {
                return Ok(BLL.StockManage.removeStock(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("RemoveConsumeStock")]
        public object RemoveConsumeStock(DAL.DTO.StockQuantity stock)
        {
            try
            {
                return Ok(BLL.StockManage.removeConsumeStock(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("TransferStockDepartment")]
        public object TransferStockDepartment(DAL.DTO.TransferStockDepartment stock)
        {
            try
            {
                return Ok(BLL.StockManage.transferStockDepartment(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


        /* [Authorize]*/
        [HttpPost("TransferStockLocation")]
        public object TransferStockLocation(DAL.DTO.TransferStockLocation stock)
        {
            try
            {
                return Ok(BLL.StockManage.transferStockLocation(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        //Scanner Api 
        /* [Authorize]*/
        [HttpPost("ScannerTransferStockDepartment")]
        public object ScannerTransferByDepartment(DAL.DTO.TransferStockDepartment stock)
        {
            try
            {
                return Ok(BLL.StockManage.scannerTransferByDepartment(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


        /* [Authorize]*/
        [HttpPost("ScannerTransferStockLocation")]
        public object ScannerTransferByLocation(DAL.DTO.TransferStockLocation stock)
        {
            try
            {
                return Ok(BLL.StockManage.scannerTransferByLocation(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*//*
        [HttpPost("SetStock")]
        public object SetStock(DAL.DTO.StockQuantity stock)
        {
            try
            {
                return Ok(BLL.StockManage.setStock(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }*/

        /*[Authorize]*/
        [HttpGet("GetStockPrice")]
        public object GetStockPrice(int id)
        {
            try
            {
                return Ok(BLL.StockManage.getStockPrice(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetStockUOM")]
        public object GetStockUOM(int id)
        {
            try
            {
                return Ok(BLL.StockManage.getStockUOM(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }       
        
        /*[Authorize]*/
        [HttpGet("GetMinThreshold")]
        public object GetMinThreshold(int id)
        {
            try
            {
                return Ok(BLL.StockManage.getMinThreshold(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("CheckSKU")]
        public object CheckSKU(string code, int id)
        {
            try
            {
                return Ok(BLL.StockManage.CheckSKU(code,id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetMixRecipe")]
        public object GetMixRecipe(int id)
        {
            try
            {
                return Ok(BLL.StockManage.getMixRecipe(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        [HttpPost("MixStock")]
        public object MixStock(DAL.DTO.StockRecipe stock)
        {
            try
            {
                return Ok(BLL.StockManage.mixStock(stock));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetCurrency")]
        public object GetCurrency(int id)
        {
            try
            {
                return Ok(BLL.StockManage.GetCurrency(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getStockByBarcode")]
        public object getStockByBarcode(int id)
        {
            try
            {
                return Ok(BLL.StockManage.getStockByBarcode(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
    }
}
