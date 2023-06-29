using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
       

        /*[Authorize]*/
        [HttpGet("Birthdays")]
        public object Birthdays()
        {
            try
            {
                return Ok(BLL.Dashboard.getBirthdays());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("FlaggedItems")]
        public object FlaggedItems()
        {
            try
            {
                return Ok(BLL.Dashboard.getFlaggedItems());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("PriceLookUpBadge")]
        public int PriceLookUpBadge()
        {
            try
            {
                return BLL.Dashboard.getPriceLookupBadge();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return 0;
            }

        }

        /*[Authorize]*/
        [HttpGet("PrintingBadge")]
        public int PrintingBadge()
        {
            try
            {
                return BLL.Dashboard.getPrintingBadge();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return 0;
            }

        }

        /*[Authorize]*/
        [HttpGet("StockItems")]
        public object StockItems()
        {
            try
            {
                return Ok(BLL.Dashboard.getWeeklyStockItems());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("TotalStockItems")]
        public object TotalStockItems()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalStockItems());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("MonthlyStockValue")]
        public object MonthlyStockValue()
        {
            try
            {
                return Ok(BLL.Dashboard.getMonthlyStockValue());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }


        [HttpGet("GetMonthlyStockPercentage")]
        public object GetMonthlyStockPercentage()
        {
            try
            {
                return Ok(BLL.Dashboard.getMonthlyStockPercentage());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
        /*[Authorize]*/
        [HttpGet("ProductuctionStoreValue")]
        public object getProductuctionStoreValue()
        {
            try
            {
                return Ok(BLL.Dashboard.getProductuctionStoreValue());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        /*[Authorize]*/
        [HttpGet("TotalDepartments")]
        public object TotalDepartments()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalDepartments());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("TotalLocations")]
        public object TotalLocations()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalLocations());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("MinStockThreshold")]
        public object MinStockThreshold()
        {
            try
            {
                return Ok(BLL.Dashboard.getMinStockThreshold());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("DepartmentStock")]
        public object DepartmentStock()
        {
            try
            {
                return Ok(BLL.Dashboard.getDepartmentStock());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));

            }
        }

        [HttpGet("GetProductionStock")]
        public object getProductionStock()
        {
            try
            {
                return Ok(BLL.Dashboard.getProductionStock());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));

            }
        }
        /*[Authorize]*/
        [HttpGet("Orders")]
        public object Orders()
        {
            try
            {
                return Ok(BLL.Dashboard.getOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("FilteredOrders")]
        public object FilteredOrders(int userId)
        {
            try
            {
                return Ok(BLL.Dashboard.getFilteredOrders(userId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("OrdersReview")]
        public object OrdersReview()
        {
            try
            {
                return Ok(BLL.Dashboard.getOrdersReview());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("FilteredOrdersReview")]
        public object FilteredOrdersReview(int userId)
        {
            try
            {
                return Ok(BLL.Dashboard.getFilteredOrdersReview(userId));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
        /*[Authorize]*/
        [HttpGet("CurrencyUSD")]
        public object getCurrencyUSD()
        {
            try
            {
                return Ok(DAL.Dashboard.CurrencyUSD);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
           
        }
        /*[Authorize]*/
        [HttpGet("CurrencyEUR")]
        public object getCurrencyEUR()
        {
            try
            {
                return Ok(DAL.Dashboard.CurrencyEUR);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        /*[Authorize]*/
        [HttpGet("PrevCurrencyUSD")]
        public object getPrevCurrencyUSD()
        {
            try
            {
                return Ok(DAL.Dashboard.PreviousUSD);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }
        /*[Authorize]*/
        [HttpGet("PrevCurrencyEUR")]
        public object getPrevCurrencyEUR()
        {
            try
            {
                return Ok(DAL.Dashboard.PreviousEUR);
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        /*[Authorize]*/
        [HttpGet("Stocktake")]
        public object getStocktake()
        {
            try
            {
                return Ok(BLL.Dashboard.getStocktake());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getTotalApprovedOrders")]
        public object getTotalApprovedOrders()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalApprovedOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getTotalPendingOrders")]
        public object getTotalPendingOrders()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalPendingOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getTotalPendingPriceOrders")]
        public object getTotalPendingPriceOrders()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalPendingPriceOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getTotalReviewOrders")]
        public object getTotalReviewOrders()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalReviewOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        /*[Authorize]*/
        [HttpGet("getTotalDraftOrders")]
        public object getTotalDraftOrders()
        {
            try
            {
                return Ok(BLL.Dashboard.getTotalDraftOrders());
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }

        }

        [HttpGet("GetDonutData")]
        public async Task<IActionResult> GetDonutData()
        {
            try
            {
                return Ok(BLL.Dashboard.GetDonutData());
            }
            catch (Exception e)
            {

                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpGet("GetPurchaseValue")]
        public async Task<IActionResult> GetPurchaseValue()
        {
            try
            {
                return Ok(BLL.Dashboard.getPurchaseValue());
            }
            catch (Exception e)
            {

                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
        [HttpGet("GetDepartmentContribution")]
        public async Task<IActionResult> GetDepartmentContribution()
        {
            try
            {
                return Ok(BLL.Dashboard.getDepartmentContribution());
            }
            catch (Exception e)
            {

                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }
    }
}
