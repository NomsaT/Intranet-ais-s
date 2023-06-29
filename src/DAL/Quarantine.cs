using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;

namespace DAL
{

    public class Quarantine
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        
        public static void ReturnToSupplier(List<DAL.DTO.Stock> ReturnData)
        {
            try
            {
                DAL.Models.AISContext db = new DAL.Models.AISContext();
               
                foreach (var item in ReturnData)
                {
                    var returnedItem = new DAL.Models.ReturnToSupplier
                    {
                        Code = item.Code,
                        ProductName = item.ProductName,
                        InternalSku = "12",/*item.InternalSku,*/
                        InternalProductName = item.InternalProductName,
                        ReturnedQuantity = item.Quantity,
                        SupplierName = item.SupplierId.ToString(),
                        DateReturned = DateTime.Now,
                        Price = item.Price
                    };

                    db.ReturnToSuppliers.Add(returnedItem);
                    db.SaveChanges();

                    var removeItem = new DAL.Models.StockQuantity
                    {
                        Id = item.Id
                    };

                    db.StockQuantities.Remove(removeItem);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }

        public static void LogReturnToSupplier(List<DAL.DTO.Stock> ReturnData)
        {
            try
            {
                DAL.Models.AISContext db = new DAL.Models.AISContext();

                foreach (var item in ReturnData)
                {
                    var ReturnedItem = db.ReturnToSuppliers.FirstOrDefault(i => i.Id == item.Id);
                    ReturnedItem.LogEntry = true;

                    db.ReturnToSuppliers.Update(ReturnedItem);
                    db.SaveChanges();                   
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                throw e;
            }
        }
    }
}
