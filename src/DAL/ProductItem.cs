using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class ProductItem
    {
        public static IQueryable<DAL.DTO.ProductItem> getProductItem()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ProductItems
               .Select(p => new DAL.DTO.ProductItem
               {
                   Id = p.Id,
                   ProductId = p.ProductId,
                   ItemId = p.ItemId,
                   Quantity = p.Quantity

               });
            return source;
        }

        public static int addProductItem(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.ProductItem();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.ProductItems.Where(m => m.ItemId == Obj.ItemId && m.ProductId == Obj.ProductId).FirstOrDefault();
            if (check != null)
            {
                throw new ProductItemException("Product Item already exists for this product.");
            }
            db.ProductItems.Add(Obj);
            
            db.SaveChanges();
            return Obj.Id;
        }

        public static object GetTotalProductItem(int productId)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ProductItems.Where(s => s.ProductId == productId).Count();
            return source;
        }

        public static object RemoveAllProductItem(int productId)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var productObj = db.Products.FirstOrDefault(s => s.Id == productId);
            db.ProductItems.RemoveRange(productObj.ProductItemProducts);
            productObj.ProductItemProducts.Clear();
            db.SaveChanges();
            return productObj;
        }

        public static async Task<int> editProductItem(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.ProductItems.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductItemException("Product Item for the product does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.ProductItems.Where(m => m.ItemId == Obj.ItemId && m.ProductId == Obj.ProductId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new ProductItemException("Product Item already exists for this product.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteProductItem(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.ProductItems.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductItemException("Product Item for the product does not exist.");

            db.ProductItems.Remove(Obj);
            await db.SaveChangesAsync();

            return "product Item removed from this product";
        }

    }
}
