using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class ProductItem
    {
        public static IQueryable<DAL.DTO.ProductItem> getProductItem()
        {
            return DAL.ProductItem.getProductItem();
        }

        public static object GetTotalProductItem(int productId)
        {
            return DAL.ProductItem.GetTotalProductItem(productId);
        }

        public static object RemoveAllProductItem(int productId)
        {
            return DAL.ProductItem.RemoveAllProductItem(productId);
        }

        public static int addProductItem(string values)
        {
            return DAL.ProductItem.addProductItem(values);
        }
        public static Task<int> editProductItem(int key, string values)
        {
            return DAL.ProductItem.editProductItem(key, values);
        }

        public static Task<string> deleteProductItem(int key)
        {
            return DAL.ProductItem.deleteProductItem(key);
        }
    }
}
