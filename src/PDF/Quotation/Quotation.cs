using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.XtraReports.UI;

namespace PDF.Quotation1
{
    public partial class Quotation
    {
        public Quotation( DAL.DTO.Quotation data)
        {
            InitializeComponent();

            HorizontalContentSplitting = DevExpress.XtraPrinting.HorizontalContentSplitting.Smart;

            POStreetAddress1Lbl.Text = "3 - 5 Struanway, Struandale";
            POCity1Lbl.Text = "Port Elizabeth";
            POWorkLandlineNo1Lbl.Text = "041 451 1550";


            SupplierLbl.Text = "CLIENTS DETAILS:";
            CompanyNameLbl.Text = "To:";
            POSupplierStreetAddressLbl.Text = data.CustomerAddressLine1 + " " + data.CustomerAddressLine2;
            POSupplierSuburbLbl.Text = data.CustomerSuburb;
            POSupplierCityLbl.Text = data.CustomerCity;
            POSupplierPostalCodeLbl.Text = data.CustomerPostalCode;

            POHeadingLbl.Text = "QUOTATION";
            PONumberLbl.Text = "Revision No:";

            for (int i = 0; i < data.QuoteRevisions.Count; i++)
            {
                PONumberValueLbl.Text = data.QuoteRevisions[i].RevisionNr.ToString(); 
            }
            PODateCreatedLbl.Text = "DATE:";
            PODateCreatedValueLbl.Text = data.DateCreated.Date.ToString("MMMM dd, yyyy");


            DeliveryToLbl.Text = "ADDRESS DETAILS:";
            PORequesterCompanyNameLbl.Text = data.CustomerName;

            if(data.CustomerPerson != null)
            {
                PORequesterPlantLocationStreetAddressLbl.Text = data.CustomerPerson;
            }
            else
            {
                PORequesterPlantLocationStreetAddressLbl.Text = "n/a";
            } 

            if(data.CustomerEmail != null)
            {
                EmailLbl.Text = data.CustomerEmail;
            }
            else
            {
                EmailLbl.Text = "n/a";
            }
            
            if(data.CustomerTelephone != null)
            {
                TelephoneLbl.Text = data.CustomerTelephone;
            }
            else
            {
                TelephoneLbl.Text = "n/a";
            }

            NetTotalbl.Text = "NETT TOTAL";
            POSubTotalValueLbl.Text = data.TotalAndVAT.ToString("c2");


            VatLbl.Text = $"VAT ({data.vatPerc}%)";
            POVATLbl.Text = data.VAT.ToString("c2");

            GrossTotalLbl.Text = "TOTAL";
            POTotalValueLbl.Text = data.Total.ToString("c2");


            InstructLbl1.Text = "PAYMENT TERMS: OPTION 1";
            InstructLbl2.Text = "30 days from date of statement, with 2.5% settlement discount - Based on account been opened and getting CGI approval.For cash deposits, a 1.8 % transaction fee will be charged";
            InstructLbl3.Text = "PAYMENT TERMS: OPTION 2";
            InstructLbl4.Text = "50% Deposit and balance before collection with a 2.5% settlement discount. For cash deposits, a 1.8 % transaction fee will be charged";
            InstructLbl5.Text = "PAYMENT TERMS: OPTION 3";
            InstructLbl6.Text = "50% Deposit and balance before collection with a 2.5% settlement discountFor cash deposits, a 1.8% transaction fee will be charged";

           
            Products.BeginInit();
            int index = 0;
            for (int i = 0; i < data.QuoteItems.Count; i++)
            {
                index = i + 1;
                XRTableRow row = new XRTableRow();

                Products.Rows.Add(row);

                XRTableCell cell1 = new XRTableCell();
                cell1.Text = data.QuoteItems[i].ProductName.ToString();
                cell1.WidthF = 217;

                XRTableCell cell2 = new XRTableCell();
                cell2.Text = data.QuoteItems[i].Width.ToString();
                cell2.WidthF = 72;

                XRTableCell cell3 = new XRTableCell();
                cell3.Text = data.QuoteItems[i].Length.ToString();
                cell3.WidthF = 62;

                XRTableCell cell4 = new XRTableCell();
                cell4.Text = data.QuoteItems[i].m2Coil.ToString();
                cell4.WidthF = 76;

                XRTableCell cell5 = new XRTableCell();
                cell5.Text = data.QuoteItems[i].priceCoil.ToString("c2");
                cell5.WidthF = 94;

                XRTableCell cell6 = new XRTableCell();
                cell6.Text = data.QuoteItems[i].FormattedAmount.ToString();
                cell6.WidthF = 85;

                XRTableCell cell7 = new XRTableCell();
                cell7.Text = data.QuoteItems[i].Quantity.ToString();
                cell7.WidthF = 37;

                XRTableCell cell8 = new XRTableCell();
                cell8.Text = data.QuoteItems[i].Total.ToString("c2");
                cell8.BackColor = System.Drawing.Color.FromArgb(245, 246, 247);
                cell8.WidthF = 117;

                Products.Rows[index].Cells.AddRange(new[] { cell1, cell2, cell3, cell4, cell5, cell6,cell7,cell8 });

            }

            Products.EndInit();

            if (data.QuoteTransports.Count > 0)
            {
            QTransport.BeginInit();
            int indexj = 0;
            for (int j = 0; j < data.QuoteTransports.Count; j++)
            {
                indexj = j + 1;
                XRTableRow row = new XRTableRow();

                QTransport.Rows.Add(row);
                XRTableCell cell1 = new XRTableCell();
                cell1.Text = data.QuoteTransports[j].Description.ToString();
                cell1.WidthF = 427;

                XRTableCell cell2 = new XRTableCell();
                cell2.Text = data.QuoteTransports[j].FormattedPrice.ToString();
                cell2.WidthF = 144;

                XRTableCell cell3 = new XRTableCell();
                cell3.Text = data.QuoteTransports[j].Quantity.ToString();
                cell3.WidthF = 65;

                XRTableCell cell4 = new XRTableCell();
                cell4.Text = data.QuoteTransports[j].Total.ToString("c2");
                cell4.BackColor = System.Drawing.Color.FromArgb(245, 246, 247);
                cell4.WidthF = 124;

                QTransport.Rows[indexj].Cells.AddRange(new[] { cell1, cell2, cell3,cell4 });
            }

            QTransport.EndInit();
            }
            else
            {
                QTransport.Visible = false;
            }
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
