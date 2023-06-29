using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet]
        public async Task<object> Get(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.Customer.getCustomers(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        [HttpPost]
        public async Task<object> Post([FromForm] string values)
        {
            try
            {
                return Ok(await BLL.Customer.addCustomer(values));
            }
            catch (Exception e)
            {
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


        [HttpPut]
        public async Task<object> Put([FromForm] int key, [FromForm] string values)
        {
            try
            {
                return Ok(await BLL.Customer.updateCustomer(key, values));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


        [HttpDelete]
        public async Task<object> Delete([FromForm] int key)
        {
            try
            {
                return Ok(await BLL.Customer.deleteCustomer(key));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }


    }
}
