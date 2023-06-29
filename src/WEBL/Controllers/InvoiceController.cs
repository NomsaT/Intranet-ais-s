using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Invoice.getInvoice(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetInvoiceableItems")]
        public async Task<IActionResult> GetInvoiceableItems(int InternalOrderId)
        {
            try
            {
                return Ok(BLL.Invoice.GetInvoiceableItems(InternalOrderId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpPost("GetInvoiceInternalOrderData")]
        /*public async Task<IActionResult> GetInvoiceInternalOrderData(DAL.DTO.ToBeInvoicedItems ToBeInvoicedItems)*/
        public object GetInvoiceInternalOrderData(DAL.DTO.ToBeInvoicedItems ToBeInvoicedItems)
        {
            try
            {
                return Ok(BLL.Invoice.GetInvoiceInternalOrderData(ToBeInvoicedItems));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateInvoice")]
        public async Task<object> AddUpdateInvoice(DAL.DTO.Invoice invoice)
        {
            try
            {
                return Ok(BLL.Invoice.AddUpdateInvoice(invoice));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpGet("RemoveInvoice")]
        public object RemoveInvoice(int id)
        {
            try
            {
                return Ok(BLL.Invoice.RemoveInvoice(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetCapturedGrnItems")]
        public async Task<IActionResult> GetCapturedGrnItems(int id)
        {
            try
            {
                return Ok(BLL.Invoice.GetCapturedGrnItems(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
