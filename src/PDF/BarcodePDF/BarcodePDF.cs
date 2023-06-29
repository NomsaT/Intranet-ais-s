using DevExpress.XtraReports.UI;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace PDF.BarcodePDF
{
    public partial class BarcodePDF
    {
        /*public BarcodePDF(DAL.DTO.BarcodePDFTemp data)*/
        public BarcodePDF()
        {
            InitializeComponent();

            InstructLbl1.Text = "1. Please send one copy of your invoice.";
            InstructLbl2.Text = "2. Enter this order in accordance with the prices, terms, delivery method & specifications listed above.";
            InstructLbl3.Text = "3. Please notify us immediately if you are not able to deliver on time.";
            InstructLbl4.Text = "4. Send all correspondence to:";


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