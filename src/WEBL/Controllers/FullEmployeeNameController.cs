using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullEmployeeNameController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            return Ok(await DataSourceLoader.LoadAsync(BLL.FullEmployeeName.getFullEmployeeName(), loadOptions));
        }

        /*[Authorize]*/
        [HttpGet("GetAllMangers")]
        public object getAllMangers()
        {
            try
            {
                return Ok(BLL.FullEmployeeName.getAllMangers());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("DepartmentManagers")]
        public async Task<IActionResult> DepartmentManagers(DataSourceLoadOptions loadOptions)
        {
            return Ok(await DataSourceLoader.LoadAsync(BLL.FullEmployeeName.getFullEmployeeNameTitle(), loadOptions));
        }

        /*[Authorize]*/
        [HttpGet("getDepartmentManagersFullName")]
        public object getDepartmentManagersFullName(int departmentId)
        {
            try
            {
                return BLL.FullEmployeeName.getDepartmentManagersFullName(departmentId);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getFilteredDepartmentManagers")]
        public async Task<IActionResult> getFilteredDepartmentManagers(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.FullEmployeeName.getFilteredDepartmentManagers(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
