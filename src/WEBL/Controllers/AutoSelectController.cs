using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoSelectController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet("GetNewestSupplier")]
        public object GetNewestSupplier()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestSupplier());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestStockCategory")]
        public object GetNewestStockCategory()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestStockCategory());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestStockGroup")]
        public object GetNewestStockGroup()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestStockGroup());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestDepartment")]
        public object GetNewestDepartment()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestDepartment());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestLocation")]
        public object GetNewestLocation()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestLocation());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestUom")]
        public object GetNewestUom()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestUom());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestPaymentMethod")]
        public object GetNewestPaymentMethod()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestPaymentMethod());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestBankName")]
        public object GetNewestBankName()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestBankName());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestCostType")]
        public object GetNewestCostType()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestCostType());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("GetNewestStorageType")]
        public object GetNewestStorageType()
        {
            try
            {
                return Ok(BLL.AutoSelect.getNewestStorageType());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }        
    }
}
