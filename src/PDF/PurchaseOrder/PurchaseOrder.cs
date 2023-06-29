using DevExpress.DataAccess.DataFederation;
using DevExpress.XtraReports.UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;

namespace PDF.PurchaseOrder
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder(DAL.DTO.PurchaseOrder data)
        {
            InitializeComponent();

            //GradientHeaderBackground.Image = CreateHeaderLineImage(753, 133, "#a5cbec", "#0000ffff");
            //GradientFooterBackground.Image = CreateHeaderLineImage(753, 133, "#0000ffff", "#a5cbec");
            HorizontalContentSplitting = DevExpress.XtraPrinting.HorizontalContentSplitting.Smart;

            POStreetAddress1Lbl.Text = "3 - 5 Struanway, Struandale";
            POCity1Lbl.Text = "Port Elizabeth";
            POWorkLandlineNo1Lbl.Text = "041 451 1550";


            //SupplierLbl.Text = "SUPPLIER:";
            POSupplierCompanyNameLbl.Text = data.CompanyName.ToString();
            POSupplierStreetAddressLbl.Text = data.SupplierPhysicalStreetAddress1;
            POSupplierSuburbLbl.Text = data.SupplierSuburb;
            POSupplierCityLbl.Text = data.SupplierPhysicalCity;
            POSupplierPostCodeLbl.Text = data.SupplierPostCode;


            POHeadingLbl.Text = "PURCHASE ORDER";
            PONumberLbl.Text = "PO NO:";
            PONumberValueLbl.Text = data.PONumber.ToString();
            QuoteNumberLbl.Text = "Quotation NO:";
            QuoteNumberValueLbl.Text = data.QuotationNumber != null ? data.QuotationNumber.ToString() : "n/a";
            //Companylbl.Text = "COMPANY:";
            companyNamelbl.Text = data.CompanyInternalName != null ? data.CompanyInternalName.ToString() : "n/a";
            PODateCreatedLbl.Text = "DATE:";
            PODateCreatedValueLbl.Text = data.DateCreated.Date.ToString("MMMM dd, yyyy");


            DeliveryToLbl.Text = "DELIVERY TO:";
            PORequesterNameSurnameLbl.Text = data.RequestByFullName;
            PORequesterCompanyNameLbl.Text = "AMFI Composites";
            PORequesterPlantLocationStreetAddressLbl.Text = data.PlantLocationStreetAddress1;
            PORequesterSuburbLbl.Text = data.PlantLocationSuburb;
            PORequesterPlantLocationPhysicalCityLbl.Text = data.PlantLocationPhysicalCity;
            PORequesterPostCodeLbl.Text = data.PlantLocationPostalCode;
            PORequesterWorkLandlineNoLbl.Text = "041 451 1550";

            DueDateLbl.Text = "Due Date:";
            PODueDateValueLbl.Text = data.DeliveryDate.Date.ToString("MMMM dd, yyyy");


            DeliveryCostLbl.Dispose();
            PODeliveryCostValueLbl.Dispose();

            discountlbl.Dispose();
            PODiscountLbl.Dispose();

            var ci = new CultureInfo("en-ZA");

            SubTotalbl.Text = "SUBTOTAL";
            POSubTotalValueLbl.Text = data.Total.ToString("C", ci);


            VatLbl.Text = "VAT";
            // POVatValueLbl.Text = data.VAT.ToString("#.00");

            POVatValueLbl.Text = data.VAT>0? DAL.PurchaseOrder.getCurrentVat() + "%": "0.00%";
            //POVatValueLbl.Text = "15%";


            TotalLbl.Text = "TOTAL";
            POTotalValueLbl.Text = data.TotalAndVAT.ToString("C", ci);


            InstructLbl1.Text = "1. Please send one copy of your invoice.";
            InstructLbl2.Text = "2. Enter this order in accordance with the prices, terms, delivery method & specifications listed above.";
            InstructLbl3.Text = "3. Please notify us immediately if you are not able to deliver on time.";
            InstructLbl4.Text = "4. Send all correspondence to:";

            POTable.BeginInit();
            int index = 0;
            for (int i = 0; i < data.InternalOrderItems.Count; i++)
            {
                index = i + 1;
                XRTableRow row = new XRTableRow();

                POTable.Rows.Add(row);

                XRTableCell cell1 = new XRTableCell();
                cell1.Text = data.InternalOrderItems[i].Code != null ? data.InternalOrderItems[i].Code.ToString() : "";
                cell1.WidthF = 75;

                XRTableCell cell2 = new XRTableCell();
                cell2.Text = data.InternalOrderItems[i].ProductName != null ? data.InternalOrderItems[i].ProductName.ToString() : "";
                cell2.WidthF = 269;

                XRTableCell cell3 = new XRTableCell();
                cell3.Text = data.InternalOrderItems[i].Quantity != null ? data.InternalOrderItems[i].Quantity.ToString() : "";
                cell3.WidthF = 122;

                XRTableCell cell4 = new XRTableCell();
                cell4.Text = data.InternalOrderItems[i].Value != null ? data.InternalOrderItems[i].Value.ToString("C", ci) : "";
                cell4.WidthF = 144;

                XRTableCell cell6 = new XRTableCell();
                cell6.Text = data.InternalOrderItems[i].Total != null ? data.InternalOrderItems[i].Total.ToString("C", ci) : "";
                cell6.BackColor = System.Drawing.Color.FromArgb(215, 228, 242);
                cell6.WidthF = 120;

                POTable.Rows[index].Cells.AddRange(new[] { cell1, cell2, cell3, cell4, cell6 });
                
            }
            int indexj = index+1;
            index++;
            for (int j = 0; j < data.onceOffItems.Count; j++)
            {
                XRTableRow row = new XRTableRow();

                POTable.Rows.Add(row);
                XRTableCell cell1 = new XRTableCell();
                cell1.Text = data.onceOffItems[j].Code != null ? data.onceOffItems[j].Code.ToString() : "";
                cell1.WidthF = 75;

                XRTableCell cell2 = new XRTableCell();
                cell2.Text = data.onceOffItems[j].Description != null ? data.onceOffItems[j].Description.ToString() : "";
                cell2.WidthF = 269;

                XRTableCell cell3 = new XRTableCell();
                cell3.Text = data.onceOffItems[j].Quantity != null ? data.onceOffItems[j].Quantity.ToString() : "";
                cell3.WidthF = 122;

                XRTableCell cell4 = new XRTableCell();

                decimal value = data.onceOffItems[j].Value ?? -1;
                cell4.Text = data.onceOffItems[j].Value != null ? value.ToString("C", ci) : "";
                cell4.WidthF = 144;

                XRTableCell cell6 = new XRTableCell();

                decimal total= data.onceOffItems[j].Total ?? -1;
                cell6.Text = data.onceOffItems[j].Total != null ? total.ToString("C",ci) : "";

                cell6.BackColor = System.Drawing.Color.FromArgb(215, 228, 242);
                cell6.WidthF = 120;
                POTable.Rows[indexj+j].Cells.AddRange(new[] { cell1, cell2, cell3, cell4, cell6 });
                index++;
                
            }
            int indexk = index+0;
            index++;
            for (int k = 0; k < data.services.Count; k++)
            {
                
                XRTableRow row = new XRTableRow();

                POTable.Rows.Add(row);

                XRTableCell cell1 = new XRTableCell();                
                cell1.Text = data.services[k].Code != null ? data.services[k].Code.ToString() : "";                
                cell1.WidthF = 75;

                XRTableCell cell2 = new XRTableCell();
                cell2.Text = data.services[k].Description != null ? data.services[k].Description.ToString() : "";
                cell2.WidthF = 269;

                XRTableCell cell3 = new XRTableCell();
                cell3.Text = data.services[k].Quantity != null ? data.services[k].Quantity.ToString() : "";
                cell3.WidthF = 122;

                XRTableCell cell4 = new XRTableCell();
                decimal value = data.services[k].Value ?? -1;
                cell4.Text = data.services[k].Value != null ? value.ToString("C", ci): "";
                cell4.WidthF = 144;

                XRTableCell cell6 = new XRTableCell();

            

                decimal? total2 = data.services[k].Value * data.services[k].Quantity?? -1;

                decimal total3 = total2 ?? -1;

                cell6.Text = total3 != -1 ? total3.ToString("C", ci) : "";

                cell6.BackColor = System.Drawing.Color.FromArgb(215, 228, 242);
                cell6.WidthF = 120;
                POTable.Rows[indexk + k].Cells.AddRange(new[] { cell1, cell2, cell3, cell4, cell6 });
                index++;
                
            }
            
            POTable.EndInit();

        }
    

        private void InternalOrder_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private static Image CreateHeaderLineImage(int width, int height, string sourceColor, string destColor)
        {

            var brushRectangle = new Rectangle(0, 0, width + 2, height);
            var rectangle = new Rectangle(0, 0, width, height);
            var bitmap = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bitmap))
            using (var gradientBrush = new LinearGradientBrush(
                brushRectangle,
                ColorTranslator.FromHtml(sourceColor),
                ColorTranslator.FromHtml(destColor),
                LinearGradientMode.Vertical))
            {
                graphics.FillRectangle(gradientBrush, rectangle);
                return bitmap;
            }
        }
    }
}