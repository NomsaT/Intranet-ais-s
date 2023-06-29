using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
       

        /*[Authorize]*/
        [HttpGet("Orders")]
        public object Orders()
        {
            try
            {
                return Ok(BLL.Chart.getOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("DepartmentStock")]
        public object DepartmentStock()
        {
            try
            {
                return Ok(BLL.Chart.getDepartmentStock());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));

            }
        }
       
    }
}
