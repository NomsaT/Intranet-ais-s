using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternalOrderController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getInternalOrder(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetInternalOrderData")]
        public async Task<IActionResult> GetInternalOrderData(int id, int action)
        {
            try
            {
                return Ok(BLL.InternalOrder.GetInternalOrderData(id, action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetApprovedInternalOrders")]
        public async Task<IActionResult> GetApprovedInternalOrders(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedInternalOrders(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetInternalOdersByDateCreated")]
        public async Task<IActionResult> GetInternalOdersByDateCreated(DataSourceLoadOptions loadOptions, DateTime  startDate, DateTime endDate)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getInternalOdersByDateCreated(startDate,endDate), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


        [HttpGet("GetAllInternalOrders")]
        public object Orders()
        {
            try
            {
                return Ok(BLL.InternalOrder.getAllOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /* [Authorize]*/
        [HttpPost("AddUpdateInternalOrder")]
        public async Task<object> AddUpdateInternalOrder(DAL.DTO.InternalOrder internalOrder)
        {
            try
            {
                return Ok(BLL.InternalOrder.AddUpdateInternalOrder(internalOrder));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("AddUpdateDraftInternalOrder")]
        public async Task<object> AddUpdateDraftInternalOrder(DAL.DTO.InternalOrder internalOrder)
        {
            try
            {
                return Ok(BLL.InternalOrder.AddUpdateDraftInternalOrder(internalOrder));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpPost("editApproveOrder")]
        public object editApproveOrder(DAL.DTO.InternalOrderAction action)
        {
            try
            {
                return Ok(BLL.InternalOrder.editApproveOrder(action.Id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpPost("editDenyOrder")]
        public object editDenyOrder(DAL.DTO.InternalOrderAction action)
        {
            try
            {
                return Ok(BLL.InternalOrder.editDenyOrder(action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpPost("editReviewOrder")]
        public object editReviewOrder(DAL.DTO.InternalOrderAction action)
        {
            try
            {
                return Ok(BLL.InternalOrder.editReviewOrder(action));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }        

        /*[Authorize]*/
        [HttpGet("getApprovedGRNInternalOrder")]
        public async Task<IActionResult> getApprovedGRNInternalOrder(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedGRNInternalOrder(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getApprovedInternalOrder")]
        public async Task<IActionResult> getApprovedInternalOrder(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedInternalOrder(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getApprovedInternalOrderAppClose")]
        public async Task<IActionResult> getApprovedInternalOrderAppClose(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedInternalOrderAppClose(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        
        /*[Authorize]*/
        [HttpGet("getApprovedInternalOrderGrn")]
        public async Task<IActionResult> getApprovedInternalOrderGrn(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedInternalOrderGrn(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getApprovedInternalOrderInvoice")]
        public async Task<IActionResult> getApprovedInternalOrderInvoice(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.InternalOrder.getApprovedInternalOrderInvoice(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("GetMonitoredItems")]
        public object GetMonitoredItems(int id)
        {
            try
            {
                return Ok(BLL.InternalOrder.GetMonitoredItems(id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("deleteDraftOrder")]
        public object deleteDraftOrder(int Id)
        {
            try
            {
                return Ok(BLL.InternalOrder.deleteDraftOrder(Id));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getDefaultCompany")]
        public object getDefaultCompany()
        {
            try
            {
                return Ok(BLL.InternalOrder.getDefaultCompany());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
    }
}
