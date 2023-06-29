using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProductStock
    {
        public static IQueryable<DAL.DTO.ProductStock> getProductStock()
        {
            return DAL.ProductStock.getProductStock();
        }
        public static object GetTotalProductStock(int productId)
        {
            return DAL.ProductStock.GetTotalProductStock(productId);
        }

        public static object RemoveAllProductStock(int productId)
        {
            return DAL.ProductStock.RemoveAllProductStock(productId);
        }

        public static int addProductStock(string values)
        {
            return DAL.ProductStock.addProductStock(values);
        }
        public static Task<int> editProductStock(int key, string values)
        {
            return DAL.ProductStock.editProductStock(key, values);
        }

        public static Task<string> deleteProductStock(int key)
        {
            return DAL.ProductStock.deleteProductStock(key);
        }

        public static object getProductItemsByBarcode(int barcode, int productionStoreId)
        {
            return DAL.ProductStock.getProductItemsByBarcode(barcode, productionStoreId);
        }

        public static Boolean ConsumeProductStock(List<int> productStockIds, List<int> prodItemIds, int productionStoreId)
        {
            return DAL.ProductStock.ConsumeProductStock(productStockIds, prodItemIds, productionStoreId);
        }
    }
}
