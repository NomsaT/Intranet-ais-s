using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace DAL
{

    public class Print
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static void GeneratePrint(List<DAL.Print.PrinterDATA> PrintData)
        {
            try
            {
                DAL.Models.AISContext db = new DAL.Models.AISContext();
               
                foreach (var PDATA in PrintData)
                {
                    // string Template = AppDomain.CurrentDomain.BaseDirectory + @"\Barcode.txt";

                    string FullInternalItemName = db.Stocks.Where(i => i.Id == PDATA.StockId).Select(s => s.InternalProductName).FirstOrDefault();
                    PDATA.FullName = FullInternalItemName.Length <= 19 ? FullInternalItemName : FullInternalItemName.Substring(0, 19);

                    PDATA.Barcode = string.Format("STQ{0:D9}", int.Parse(PDATA.Barcode.Substring(3)));


                    string Template = DAL.BarcodeTemp.getBarcodeTemp().Template;
                    string sobj = JsonConvert.SerializeObject(PDATA);
                    Dictionary<string, string> dix = JsonConvert.DeserializeObject<Dictionary<string, string>>(sobj);                    
                    
                    foreach (var pair in dix)
                    {
                        Template = Template.Replace("{" + pair.Key + "}", pair.Value);
                    }

                    //Template = Template.Replace("<STX>", "\x0002");
                    //Template = Template.Replace("<ESC>", "\x001B");

                    string address = DAL.Printers.getPrinter().Name;


                    //TcpClient client = new TcpClient();
                    //client.Connect("192.168.1.50", 4578);

                    //StreamWriter writer = new StreamWriter(client.GetStream());
                    //writer.Write(Template);
                    //writer.Flush();
                    //writer.Close();
                    //client.Close();

                    RawPrinterHelper.PrintText(PDATA.Barcode, address, Template);

                    var item = db.StockQuantities.Where(a => a.Id == PDATA.Id).FirstOrDefault();

                    if (item == null)
                    {
                        throw new PrintBarcodeException("Barcode not marked as print.");
                    }

                    item.BarcodePrinted = true;
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }

        public static void GeneratePrintProduct(List<DAL.Print.PrinterDATA> PrintData)
        {
            try
            {
                DAL.Models.AISContext db = new DAL.Models.AISContext();

                foreach (var PDATA in PrintData)
                {
                    // string Template = AppDomain.CurrentDomain.BaseDirectory + @"\Barcode.txt";

                    PDATA.Barcode = string.Format("STQ{0:D9}", int.Parse(PDATA.Barcode.Substring(3)));

                    string Template = DAL.BarcodeTemp.getBarcodeProductTemp().Template;
                    string sobj = JsonConvert.SerializeObject(PDATA);
                    Dictionary<string, string> dix = JsonConvert.DeserializeObject<Dictionary<string, string>>(sobj);
                    foreach (var pair in dix)
                    {
                        Template = Template.Replace("{" + pair.Key + "}", pair.Value);
                    }

                    //Template = Template.Replace("<STX>", "\x0002");
                    //Template = Template.Replace("<ESC>", "\x001B");

                    string address = DAL.Printers.getPrinter().Name;

                    RawPrinterHelper.PrintText(PDATA.Barcode, address, Template);

                  /*  var item = db.Products.Where(a => a.Id == PDATA.Id).FirstOrDefault();

                    if (item == null)
                    {
                        throw new PrintBarcodeException("Barcode not marked as print.");
                    }*/
                    
                    db.SaveChanges();

                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }

        public class PrinterDATA
        {
            public int Id { get; set; }
            public string Barcode { get; set; }
            public string Code { get; set; }
            public string FullName { get; set; }
            public int StockId { get; set; }
        }

        public static void GeneratePrintBin(List<DAL.Print.PrinterBinDATA> PrintData)
        {
            try
            {
                DAL.Models.AISContext db = new DAL.Models.AISContext();

                foreach (var PDATA in PrintData)
                {
                    // string Template = AppDomain.CurrentDomain.BaseDirectory + @"\Barcode.txt";
                    PDATA.Barcode = string.Format("STQ{0:D9}", int.Parse(PDATA.Barcode.Substring(3)));
                    string Template = DAL.BarcodeTemp.getBarcodeBinTemp().Template;
                    string sobj = JsonConvert.SerializeObject(PDATA);
                    Dictionary<string, string> dix = JsonConvert.DeserializeObject<Dictionary<string, string>>(sobj);

                    foreach (var pair in dix)
                    {
                        Template = Template.Replace("{" + pair.Key + "}", pair.Value);
                    }

                    //Template = Template.Replace("<STX>", "\x0002");
                    //Template = Template.Replace("<ESC>", "\x001B");

                    string address = DAL.Printers.getPrinter().Name;


                    //TcpClient client = new TcpClient();
                    //client.Connect("192.168.1.50", 4578);

                    //StreamWriter writer = new StreamWriter(client.GetStream());
                    //writer.Write(Template);
                    //writer.Flush();
                    //writer.Close();
                    //client.Close();

                    RawPrinterHelper.PrintText(PDATA.Barcode, address, Template);

                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }

        public class PrinterBinDATA
        {
            public int Id { get; set; }
            public string Barcode { get; set; }
            public string Name { get; set; }
        }
    }
}
