using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScannerConfig : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet("GetScannerConfig")]
        public async Task<IActionResult> GetScannerConfig(string deviceId)
        {
            try
            {
                return Ok(BLL.ScannerConfig.getScannerConfig(deviceId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("SetScannerLocation")]
        public async Task<IActionResult> SetScannerLocation(DAL.Models.ScannerConfig scannerConfig)
        {
            try
            {
                return Ok(BLL.ScannerConfig.setScannerLocation(scannerConfig));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPut("EditScannerConfig")]
        public async Task<IActionResult> Put(string deviceId, DAL.Models.ScannerConfig values)
        {
            try
            {
                return Ok(BLL.ScannerConfig.editScannerLocation(deviceId, values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
