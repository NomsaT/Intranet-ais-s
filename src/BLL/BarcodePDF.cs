using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace BLL
{
    public static class BarcodePDF
    {
        public static Boolean generateBarcodePDF()
        {
            var name = 123 + ".PDF";
            
            PDF.BarcodePDF.BarcodePDF BarcodePDF = new PDF.BarcodePDF.BarcodePDF();
            BarcodePDF.ExportToPdf(@"C:\tmp\" + name);
            return true;
        }
    }
}
