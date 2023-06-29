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
    public class QuarantineController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();             

        /* [Authorize]*/
        [HttpPost("ReturnToSupplier")]
        public object ReturnToSupplier(List<DAL.DTO.Stock> data)
        {
            try
            {
                return Ok(BLL.Quarantine.ReturnToSupplier(data));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("LogReturnToSupplier")]
        public object LogReturnToSupplier(List<DAL.DTO.Stock> data)
        {
            try
            {
                return Ok(BLL.Quarantine.LogReturnToSupplier(data));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
