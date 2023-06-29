using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DAL.Import import = new DAL.Import();

        /*[Authorize]*/
        [HttpGet("Stock")]
        public object Stock()
        {
            try
            {
                return Ok(import.Stock());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
    }
}
