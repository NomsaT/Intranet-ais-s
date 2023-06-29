using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public static class Grn
    {
        public static IQueryable<DAL.DTO.Grn> getGrn()
        {
            return DAL.Grn.getGrn();
        }

        public static DAL.DTO.GrnOrderData GetGrnOrderData(int id, int action)
        {
            switch (action)
            {
                case (int)DAL.Constants.GrnAction.ADD:
                    {
                        return DAL.Grn.GetNewGrnInternalOrderData(id);
                    }
                case (int)DAL.Constants.GrnAction.UPDATE:
                    {
                        return DAL.Grn.GetGrnInternalOrderData(id, action);
                    }
                case (int)DAL.Constants.GrnAction.REMOVE:
                    {
                        return DAL.Grn.GetGrnInternalOrderData(id, action);
                    }
                default: throw new Exception();
            }

        }

        public static List<string> AddUpdateGrn(DAL.DTO.Grn grn)
        {
            List<string> dltBarcode = new List<string>();
            if (grn.Id > 0)
            {                
                foreach(var item in grn.GrnItems)
                {
                    var oldQty = DAL.Grn.GetQtyGrn(item.Id);
                    if(item.Quantity < oldQty)
                    {
                        
                        var initialstockremove = new DAL.DTO.StockQuantity();
                        initialstockremove.StockId = item.StockId;
                        for (int i = 0; i < oldQty - item.Quantity; i++)
                        {
                            dltBarcode.Add(""+DAL.StockManage.removeStock(initialstockremove));
                            //DAL.StockManage.removeStockItem(initialstockremove);
                        }
                    }
                    else
                    {
                        if (item.Quantity > oldQty)
                        {
                            var initialstockadd = new DAL.DTO.StockQuantity();
                            initialstockadd.StockId = item.StockId;
                            initialstockadd.ItemQuantity = item.Quantity - oldQty;
                            DAL.StockManage.addStock(initialstockadd);
                        }
                    }               
                }
                DAL.Models.Grn data = DAL.Grn.UpdateGrn(grn);
                return dltBarcode;
            }
            else
            {
                DAL.Models.Grn data = DAL.Grn.AddGrn(grn);
                foreach (var item in grn.GrnItems)
                {
                    var initialstockadd = new DAL.DTO.StockQuantity();
                    initialstockadd.StockId = item.StockId;
                    initialstockadd.ItemQuantity = item.Quantity;
                    initialstockadd.GrnNumber = data.GrnNumber;
                    DAL.StockManage.addStock(initialstockadd);
                }

                return dltBarcode;
            }


        }

        public static List<string> RemoveGrn(int id)
        {            
            return DAL.Grn.RemoveGrn(id);
        }

        public static object GetLatestGrn(int id)
        {
            return DAL.Grn.GetLatestGrn(id);
        }

    }
}
