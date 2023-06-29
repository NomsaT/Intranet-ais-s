using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotationController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Quotations.getQuotations(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetQuotationsData")]
        public async Task<IActionResult> GetQuotationsData(int id, int action)
        {
            try
            {
                return Ok(BLL.Quotations.GetQuotationsData(id, action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateQuotations")]
        public async Task<object> AddUpdateQuotations(DAL.DTO.Quote quote)
        {
            try
            {
                return Ok(BLL.Quotations.AddUpdateQuotations(quote));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateDraftQuotations")]
        public async Task<object> AddUpdateDraftQuotations(DAL.DTO.Quote quote)
        {
            try
            {
                return Ok(BLL.Quotations.AddUpdateDraftQuotations(quote));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
       
    }
}
