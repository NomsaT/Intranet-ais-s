using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncreaseTypeController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.IncreaseType.getIncreaseType(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

    }
}
