using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;
using WEBL;

namespace BaseProjectBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetPDFController : ControllerBase
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        [HttpGet]
        public IActionResult GetPDF(int Id)
        {
            try
            {        
                return File(BLL.GetPDF.getPDF(Id), "application/pdf");
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}