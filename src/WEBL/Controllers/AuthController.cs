using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [AllowAnonymous]
        [HttpPost("Login")]
        public object Login(DAL.DTO.Authenticate user)
        {
            try
            {
                return Ok(BLL.Auth.login(user));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [AllowAnonymous]
        [HttpPost("ResetPassword")]
        public async Task<object> ResetPassword(DAL.DTO.Authenticate user)
        {
            try
            {
                return Ok(await BLL.Auth.ResetPassword(user));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [AllowAnonymous]
        [HttpPost("BlockUser")]
        public object BlockUser(DAL.DTO.BlockUser user)
        {
            try
            {
                BLL.Auth.BlockUser(user);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [AllowAnonymous]
        [HttpPost("SetPassword")]
        public object SetPassword(DAL.DTO.ManageAuth user)
        {
            try
            {
                BLL.Auth.SetPassword(user);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [AllowAnonymous]
        [HttpPost("AdminSetPassword")]
        public object AdminSetPassword(DAL.DTO.SetPassword user)
        {
            try
            {
                BLL.Auth.AdminSetPassword(user);
                return Ok();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [AllowAnonymous]
        [HttpPost("UserSetPassword")]
        public object UserSetPassword(DAL.DTO.SetPassword user)
        {
            try
            {
                BLL.Auth.UserSetPassword(user);
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
