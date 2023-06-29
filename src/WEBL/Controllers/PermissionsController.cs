using Microsoft.AspNetCore.Mvc;
using NLog;
using System;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet("getRolePermissions")]
        public object getRolePermissions(int id)
        {
            try
            {
                return Ok(BLL.Permissions.getRolePermissions(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AssignRolesPermissions")]
        public object AssignRolesPermissions(DAL.DTO.UpdatePermissions updatePermissions)
        {
            try
            {
                BLL.Permissions.AssignRolesPermissions(updatePermissions.roleId, updatePermissions.permissions);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getUserPermissions")]
        public object getUserPermissions(int id)
        {
            try
            {
                return Ok(BLL.Permissions.getUserPermissions(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AssignUserPermissions")]
        public object AssignUserPermissions(DAL.DTO.UpdatePermissions updatePermissions)
        {
            try
            {
                BLL.Permissions.AssignUserPermissions(updatePermissions.userId, updatePermissions.permissions);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
