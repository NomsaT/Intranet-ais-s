using System.IO;

namespace BLL
{
    public static class GetPDF
    {
        public static MemoryStream getPDF(int Id)
        {
            /*return DAL.GetPDF.getPDF(Id);*/
            var data = DAL.PurchaseOrder.getPurchaseOrder(Id);
            var name = Id + ".PDF";

            PDF.PurchaseOrder.PurchaseOrder PDF = new PDF.PurchaseOrder.PurchaseOrder(data);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();

            PDF.ExportToPdf(stream);

            stream.Position = 0;


            return stream;
        }      
    }
}