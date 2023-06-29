using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentManager : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.DepartmentManager.getDepartmentManager(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] string values)
        {
            try
            {
                return Ok(BLL.DepartmentManager.addDepartmentManager(values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] int key, [FromForm] string values)
        {
            try
            {
                return Ok(await BLL.DepartmentManager.editDepartmentManager(key, values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromForm] int key)
        {
            try
            {
                return Ok(await BLL.DepartmentManager.deleteDepartmentManager(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
