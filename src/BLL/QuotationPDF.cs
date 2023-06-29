namespace BLL
{
    public static class QuotationPDF
    {
        public static void generatePDF(int id)
        {
            var data = DAL.QuotationPDF.getQuotation(id);
            var name = id + ".PDF";

            PDF.Quotation1.Quotation Quotation = new PDF.Quotation1.Quotation(data);
            Quotation.ExportToPdf(@"C:\tmp\" + name);
        }
    }
}
