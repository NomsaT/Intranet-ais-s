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
    public class StocktakePeriod : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpPost("AddStocktakeCycle")]
        public async Task<IActionResult> Add([FromForm] string values)
        {
            try
            {
                return Ok(BLL.StocktakePeriod.AddStocktakePeriod(values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPut("EditCycle")]
        public async Task<IActionResult> Put([FromForm] int key, [FromForm] string values)
        {
            try
            {
                return Ok(await BLL.StocktakePeriod.EditStocktakePeriodAsync(key, values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpDelete("DeleteCycle")]
        public async Task<IActionResult> Delete([FromForm] int key)
        {
            try
            {
                return Ok(await BLL.StocktakePeriod.DeleteStocktakePeriodAsync(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetStocktakeCycle(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.StocktakePeriod.GetStocktakeCycle(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
