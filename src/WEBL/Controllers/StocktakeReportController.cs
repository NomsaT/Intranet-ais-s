using Microsoft.AspNetCore.Mvc;
using DevExtreme.AspNet.Data;
using NLog;
using System;
using System.Threading.Tasks;
using DAL.DTO;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocktakeReport : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await BLL.StocktakeReport.GetStocktakeReports());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
