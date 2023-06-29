using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class PrintBarcode
    {
        public static IQueryable<DAL.DTO.StockQuantity> getPrintBarcode()
        {
            return DAL.PrintBarcode.getPrintBarcode();
        }

        public static IQueryable<DAL.DTO.StockQuantity> getReprintBarcode()
        {
            return DAL.PrintBarcode.getReprintBarcode();
        }

        public static IQueryable<DAL.DTO.StockQuantity> getBarcodeVerificationReport()
        {
            return DAL.PrintBarcode.getBarcodeVerificationReport();
        }
        
        public static int markPrinted(int barcode)
        {
            return DAL.PrintBarcode.markPrinted(barcode);
        }

        public static bool verificationScan(List<int> barcodes)
        {
            return DAL.PrintBarcode.verificationScan(barcodes);
        }
        
        public static int Printing(List<DAL.Print.PrinterDATA> data)
        {
            DAL.Print.GeneratePrint(data);
            return 1;
        }

        public static int PrintingBinBarcode(List<DAL.Print.PrinterBinDATA> data)
        {
            DAL.Print.GeneratePrintBin(data);
            return 1;
        }

        /*public static int markPrintedProduct(int barcode)
        {
            return DAL.PrintBarcode.markPrintedProduct(barcode);
        }*/

        public static int PrintingProduct(List<DAL.Print.PrinterDATA> data)
        {
            DAL.Print.GeneratePrintProduct(data);
            return 1;
        }
    }
}
