using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Stock
    {
        public static IQueryable<DAL.DTO.Stock> getStock()
        {
            return DAL.Stock.getStock();
        }

        public static IQueryable<DAL.DTO.Stock> getStockWithQty()
        {
            return DAL.Stock.getStockWithQty();
        }

        public static DAL.DTO.Stock GetStockData(int id, int action)
        {
           return DAL.Stock.getStockDataEdit(id);
        }
        public static IQueryable<DAL.DTO.Stock> getUnassignedStock(int StockId)
        {
            return DAL.Stock.getUnassignedStock(StockId);
        }

        public static int AddUpdateStock(DAL.DTO.Stock values)
        {
            if (values.Id > 0)
            {
                int result = DAL.Stock.editStock(values.Id, values);
                return result;
            }
            else
            {
                int stockId = DAL.Stock.addStock(values);
                var Obj = new DAL.DTO.Stock();
                var serializerSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
                JsonConvert.PopulateObject(JsonConvert.SerializeObject(values), Obj, serializerSettings);
                if (Obj.CurrentPrice > 0 && Obj.PackQuantity > 0)
                {
                    var lookupObj = new DAL.DTO.PriceLookup();
                    lookupObj.StockId = stockId;
                    lookupObj.SupplierId = Obj.SupplierId;
                    lookupObj.Price = Obj.ItemPrice;
                    lookupObj.Date = DateTime.Now;
                    lookupObj.Comment = "Price added via stock page.";
                    lookupObj.Username = Obj.Username;
                    string lookupObjString = JsonConvert.SerializeObject(lookupObj);
                    DAL.PriceLookUp.addPriceLookUp(lookupObjString);
                }
                return stockId;
            }


        }

        public static Task<string> deleteStock(int key)
        {
            return DAL.Stock.deleteStock(key);
        }

        public static decimal GetTotalStockDep(int id, int stockId)
        {
            return DAL.Stock.getTotalStockDep(id,stockId);
        }

        public static decimal GetTotalStockLocation(int id, int stockId)
        {
            return DAL.Stock.getTotalStockLocation(id, stockId);
        }

        public static decimal GetTotalStockStore(int locationId, int storeId, int stockId)
        {
            return DAL.Stock.getTotalStockStore(locationId, storeId,stockId);
        }

        public static object GetStockByName(string name)
        {
            return DAL.Stock.GetStockByName(name);
        }
        public static int GetStockCount(string code, int locId, int storeId)
        {
            return DAL.Stock.GetStockCount(code, locId, storeId);
        }
    }
}
