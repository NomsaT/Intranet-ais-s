using DevExtreme.AspNet.Data;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrintBarcodeController : ControllerBase
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /*[Authorize]*/
        [HttpGet("getPrintBarcode")]
        public async Task<IActionResult> getPrintBarcode(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.PrintBarcode.getPrintBarcode(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getReprintBarcode")]
        public async Task<IActionResult> getReprintBarcode(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.PrintBarcode.getReprintBarcode(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("getBarcodeVerificationReport")]
        public async Task<IActionResult> getBarcodeVerificationReport(DataSourceLoadOptions loadOptions)
        {
            try
            {
                return Ok(await DataSourceLoader.LoadAsync(BLL.PrintBarcode.getBarcodeVerificationReport(), loadOptions));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpGet("markPrinted")]
        public object markPrinted(int barcode)
        {
            try
            {
                return Ok(BLL.PrintBarcode.markPrinted(barcode));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("verificationScan")]
        public IActionResult verificationScan(List<int> barcodes)
        {
            try
            {
                return Ok(BLL.PrintBarcode.verificationScan(barcodes));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }        

        /* [Authorize]*/
        [HttpPost("Printing")]
        public object  Printing(List<DAL.Print.PrinterDATA> data)
        {
            try
            {
                return Ok(BLL.PrintBarcode.Printing(data));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*/
        [HttpPost("PrintingBinBarcode")]
        public object PrintingBinBarcode(List<DAL.Print.PrinterBinDATA> data)
        {
            try
            {
                return Ok(BLL.PrintBarcode.PrintingBinBarcode(data));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /* [Authorize]*//*
        [HttpGet("markPrintedProduct")]
        public object markPrintedProduct(int barcode)
        {
            try
            {
                return Ok(BLL.PrintBarcode.markPrintedProduct(barcode));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }*/

        /* [Authorize]*/
        [HttpPost("PrintingProduct")]
        public object PrintingProduct(List<DAL.Print.PrinterDATA> data)
        {
            try
            {
                return Ok(BLL.PrintBarcode.PrintingProduct(data));
            }
            catch (Exception e)
            {
                logger.Error(e);
                return BadRequest(ErrorMessage.GetMessage(e));
            }
        }

        /*[Authorize]*/
        [HttpGet("PrintBarcodePDF")]
        public Boolean PrintBarcodePDF()
        {
            try
            {
                return BLL.BarcodePDF.generateBarcodePDF();
            }
            catch (Exception e)
            {
                logger.Error(e);
                return false;
            }
        }
    }
}
