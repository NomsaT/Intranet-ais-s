using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceIncreaseController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpPost("PriceIncrease")]
        public object PriceIncrease(DAL.DTO.PriceIncrease increase)
        {
            try
            {
                return Ok(BLL.PriceIncrease.addPriceIncrease(increase));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpPut("PriceIncreaseEdit")]
        public object PriceIncreaseEdit(DAL.DTO.PriceIncrease increase)
        {
            try
            {
                return Ok(BLL.PriceIncrease.editPriceIncrease(increase));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getItemInfo")]
        public object getItemInfo(int id)
        {
            try
            {
                return Ok(BLL.PriceIncrease.getItemInfo(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpDelete("removeItemInfo")]
        public bool removeItemInfo(int id)
        {
            try
            {
                return BLL.PriceIncrease.removeItemInfo(id);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }

        }
    }
}
