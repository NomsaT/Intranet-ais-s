using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DBVersionController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet("getDBVersion")]
        public object getDBVersion()
        {
            try
            {
                return Ok(BLL.DBVersion.getDBVersion());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }

        }
    }
}
