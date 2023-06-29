using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DAL
{
    public class Import
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private string path, pathError;
        DAL.Models.AISContext db = new DAL.Models.AISContext();

        public List<string> Stock()
        {
            List<string> messages = new List<string>();
            messages.Add("Importing Stock");
            logger.Trace("Importing Stock");

            GetPaths("Stock");

            string[] files = Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly);
            messages.Add(files.Count() + " CSV Files");
            logger.Trace(files.Count() + " CSV Files");

            foreach (string file in files)
            {
                try
                {
                    bool state = true;

                    FileInfo fInfo = new FileInfo(file);

                    messages.Add("Reading File: " + fInfo.Name);
                    logger.Trace("Reading File: " + fInfo.Name);

                    List<string> lines = new List<string>(File.ReadAllLines(file));

                    messages.Add(lines.Count() + " Stock Items");
                    logger.Trace(lines.Count() + " Stock Items");

                    for (int i = lines.Count - 1; i >= 0; i--)
                    {
                        try
                        {
                            ImportStock(lines[i]);
                            lines.RemoveAt(i);
                        }
                        catch (Exception)
                        {
                            messages.Add("Invalid Stock : " + lines[i]);
                            logger.Trace("Invalid Stock : " + lines[i]);
                            state = false;
                        }
                    }

                    if (!state)
                    {
                        File.WriteAllLines(file, lines);
                        String errorFile = Path.Combine(pathError, fInfo.Name);
                        if (File.Exists(errorFile)) File.Delete(errorFile);
                        File.Move(file, errorFile);
                        messages.Add("Failed to Import All Stock - " + lines.Count() + " Invalid Stock");
                        logger.Trace("Failed to Import All Stock - " + lines.Count() + " Invalid Stock");
                    }
                    else
                    {
                        File.Delete(file);
                        messages.Add("Successfull Stock Import");
                        logger.Trace("Successfull Stock Import");
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            }
            return messages;
        }

        private void GetPaths(string type)
        {
            path = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Import", type);
            pathError = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Import", type, "Error");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            if (!Directory.Exists(pathError)) Directory.CreateDirectory(pathError);
        }

        private void ImportStock(string line)
        {
            string[] items = line.Split(';');

            DTO.Stock stock = new DTO.Stock();
            stock.Code = items[0];
            stock.ProductName = items[1];
            stock.Uomid = db.UnitOfMeasurements.First(d => d.Name == items[2]).Id;
            stock.StockCategoryId = db.StockCategories.First(d => d.Name == items[3]).Id;
            stock.MinThreshold = Decimal.Parse(items[4]);
            stock.StockGroupId = db.StockGroups.FirstOrDefault(d => d.Name == items[5])?.Id;

            //string data = JsonConvert.SerializeObject(stock);
            DAL.Stock.addStock(stock);
        }


    }
}
