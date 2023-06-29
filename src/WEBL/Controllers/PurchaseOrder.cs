using Microsoft.AspNetCore.Mvc;
using NLog;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrder : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

    }
}
