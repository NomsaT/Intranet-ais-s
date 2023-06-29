namespace BLL
{
    public static class PurchaseOrder
    {
        public static void generatePDF(int id)
        {
            var data = DAL.PurchaseOrder.getPurchaseOrder(id);
            var name = id + ".PDF";

            PDF.PurchaseOrder.PurchaseOrder PurchaseOrder = new PDF.PurchaseOrder.PurchaseOrder(data);
            PurchaseOrder.ExportToPdf(@"C:\tmp\" + name);
        }
    }
}
