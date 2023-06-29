using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class ProductStock
    {
        public static IQueryable<DAL.DTO.ProductStock> getProductStock()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ProductStocks
               .Select(p => new DAL.DTO.ProductStock
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   ProductId = p.ProductId,
                   Quantity = p.Quantity,
                   QtyPerSquareMeter = p.QtyPerSquareMeter,
                   SquaresUsed = p.SquaresUsed,
                   UomName = p.Stock.Uom.Name

               });
            return source;
        }

        public static int addProductStock(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.ProductStock();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.ProductStocks.Where(m => m.StockId == Obj.StockId && m.ProductId == Obj.ProductId).FirstOrDefault();
            if (check != null)
            {
                throw new ProductStockException("Product Stock already exists for this product.");
            }
            db.ProductStocks.Add(Obj);

            db.SaveChanges();
            return Obj.Id;
        }

        public static object GetTotalProductStock(int productId)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.ProductStocks.Where(s => s.ProductId == productId).Count();
            return source;
        }

        public static object RemoveAllProductStock(int productId)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var productObj = db.Products.FirstOrDefault(s => s.Id == productId);
            db.ProductStocks.RemoveRange(productObj.ProductStocks);
            productObj.ProductStocks.Clear();
            db.SaveChanges();
            return productObj;
        }

        /*TODO: Move to products @lonwabo*/
        public static Boolean ConsumeProductStock(List<int> productStockIds, List<int> productItemsIds, int productionStoreId)
        {
            Models.AISContext db = new DAL.Models.AISContext();
            var flag = false;
            string exceptionMessage = "";

            var product = new Models.Product();



            
            if (productStockIds.Count > 0)
            {
                for (int i = 0; i < productStockIds.Count; i++)
                {
                    var id = productStockIds[i];

                    var productStock = db.ProductStocks.FirstOrDefault(x => x.Id == id);

                    if (productStock != null)
                    {
                        var qty = productStock.Quantity;
                        var stock = db.Stocks.FirstOrDefault(x => x.Id == productStock.StockId);
                        if (stock != null)
                        {
                            //Todo: filter the stock qty's by prod store
                            decimal totalQty = stock.StockQuantities.Where(x => x.StoreId == productionStoreId && x.ItemQuantity > 0).Sum(x => x.ItemQuantity);

                            var stockQuantities = db.StockQuantities.Where(x => x.StockId == stock.Id && x.ItemQuantity > 0 && x.StoreId == productionStoreId);
                            product = productStock.Product;
                            if (totalQty >= productStock.Quantity && stockQuantities.Count() > 0)
                            {
                                foreach (var stockQuantity in stockQuantities)
                                {
                                    if (qty == 0)
                                    {
                                        break;
                                    }
                                    if (stockQuantity.ItemQuantity <= qty)
                                    {
                                        qty -= stockQuantity.ItemQuantity;
                                        stockQuantity.ItemQuantity = 0;
                                        db.StockQuantities.Remove(stockQuantity);
                                        flag = true;
                                    }
                                    else
                                    {
                                        stockQuantity.ItemQuantity -= qty;
                                        flag = true;
                                        db.StockQuantities.Update(stockQuantity);
                                        qty = 0;
                                    }
                                }
                            }
                            else
                            {
                                exceptionMessage = "You do not have stock in the production store";
                                throw new ProductException(exceptionMessage);
                            }
                        }
                        else
                        {
                            exceptionMessage = "BOM Item not found";
                            throw new ProductException(exceptionMessage);
                        }
                    }
                    else
                    {
                        exceptionMessage = "BOM Item not found";
                        throw new ProductException(exceptionMessage);
                    }

                }
            }

            if (productItemsIds.Count > 0)
            {
                for (int i = 0; i < productItemsIds.Count; i++)
                {
                    int productItemId = productItemsIds[i];

                    var productItem = db.ProductItems
                        .FirstOrDefault(s => s.Id == productItemId);
                    var p = db.Products.FirstOrDefault(x => x.Id == productItem.ItemId);
                    product = productItem.Product;
                    //Todo: seperate the if's
                    if (p != null)
                    {
                        if (p.Quantity != 0)
                        {
                            if (p.Quantity >= productItem.Quantity)
                            {
                                p.Quantity -= productItem.Quantity;
                                db.Products.Update(p);
                                flag = true;
                            }
                            else
                            {
                                //how to handle this.???
                                exceptionMessage = "Not enough "+productItem.Product.ProductCode + " - " + productItem.Product.ProductName + " in stock";
                                throw new ProductException(exceptionMessage);
                            }
                        }
                        else
                        {
                            exceptionMessage = "No " + productItem.Product.ProductCode + " - " + productItem.Product.ProductName + " in stock";
                            throw new ProductException(exceptionMessage);
                        }
                    }
                    else
                    {
                        exceptionMessage = "BOM Item not found";
                        throw new ProductException(exceptionMessage);
                    }
                }
            }

            if (flag)
            {
                product.Quantity += 1;
                db.Products.Update(product);
                db.SaveChanges();
            }
            else
            {
                throw new ProductException("Add BOM items");
            }
            return true;
        }

        public static async Task<int> editProductStock(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.ProductStocks.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductStockException("Product Stock for the product does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.ProductStocks.Where(m => m.StockId == Obj.StockId && m.ProductId == Obj.ProductId && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new ProductStockException("Product Stock already exists for this product.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteProductStock(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.ProductStocks.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductStockException("Product Stock for the product does not exist.");

            db.ProductStocks.Remove(Obj);
            await db.SaveChangesAsync();

            return "Product Stock removed from this product";
        }

        /*TODO: Move to products @lonwabo*/
        public static object getProductItemsByBarcode(int barcode, int productionStoreId)
        {

            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = db.Products.FirstOrDefault(o => o.Id == barcode);
            if (Obj == null) throw new ProductException("Product does not exist.");

            var source = db.ProductStocks
                .Where(x => x.ProductId == barcode)
                .Include(x => x.Stock)
                .Include(x => x.Product)
               .Select(p => new
               {
                   Id = p.Id,
                   StockId = p.StockId,
                   ProductId = p.ProductId,
                   ProductName = p.Product.ProductName,
                   Quantity = p.Quantity,
                   Name = p.Stock.InternalProductName,
               }).ToList();

            var productItems = db.ProductItems.Where(x => x.ProductId == barcode).ToList();



            return ConsumeProductStock(source.Select(s => s.Id).ToList(), productItems.Select(x => x.Id).ToList(), productionStoreId);
        }
    }
}
