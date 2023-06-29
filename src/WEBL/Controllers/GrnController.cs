using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrnController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Grn.getGrn(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetGrnInternalOrderData")]
        public async Task<IActionResult> GetGrnInternalOrderData(int id, int action)
        {
            try
            {
                return Ok(BLL.Grn.GetGrnOrderData(id, action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateGrn")]
        public async Task<object> AddUpdateGrn(DAL.DTO.Grn grn)
        {
            try
            {
                return Ok(BLL.Grn.AddUpdateGrn(grn));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpGet("RemoveGrn")]
        public object RemoveGrn(int id)
        {
            try
            {
                return Ok(BLL.Grn.RemoveGrn(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetLatestGrn")]
        public object GetLatestGrn(int id)
        {
            try
            {
                return Ok(BLL.Grn.GetLatestGrn(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
