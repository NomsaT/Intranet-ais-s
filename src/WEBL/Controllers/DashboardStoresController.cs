using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardStoresController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet("getTodaysDeliveries")]
        public object getTodaysDeliveries()
        {
            try
            {
                return Ok(BLL.DashboardStores.getTodaysDeliveries());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getUpcommingDeliveries")]
        public object getUpcommingDeliveries()
        {
            try
            {
                return Ok(BLL.DashboardStores.getUpcommingDeliveries());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getLateDeliveries")]
        public object getLateDeliveries()
        {
            try
            {
                return Ok(BLL.DashboardStores.getLateDeliveries());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /* [Authorize]*/
        [HttpGet("DeliveryReminderEmailing")]
        public object DeliveryReminderEmailing(int id, string type)
        {
            try
            {
                return Ok(BLL.DashboardStores.getDeliveryReminderEmailing(id, type));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
