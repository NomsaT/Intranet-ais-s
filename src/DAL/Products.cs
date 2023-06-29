using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public static class Products
    {
        public static IQueryable<DAL.DTO.Product> getProducts()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Products
               .Select(p => new DAL.DTO.Product
               {
                   Id = p.Id,
                   ProductCode = p.ProductCode,
                   ProductName = p.ProductName,
                   DepartmentFullName = p.Department.Abbreviation + " - " + p.Department.Name,
                   DepartmentId = p.DepartmentId,
                   Description = p.Description,
                   Quantity = p.Quantity,
                   Barcode = "PROD" + p.Id.ToString("D8")

               });
            return source;
        }

        public static int addProducts(string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var Obj = new DAL.Models.Product();
            JsonConvert.PopulateObject(values, Obj);
            var check = db.Products.Where(m => m.ProductName == Obj.ProductName).FirstOrDefault();
            if (check != null)
            {
                throw new ProductException("Product already exists.");
            }
            Obj.Quantity = 0;
            db.Products.Add(Obj);
            db.SaveChanges();
            return Obj.Id;
        }

        public static async Task<int> editProducts(int key, string values)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Products.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductException("Product does not exist.");

            JsonConvert.PopulateObject(values, Obj);
            var check = db.Products.Where(m => m.ProductName == Obj.ProductName && m.Id != Obj.Id).FirstOrDefault();
            if (check != null)
            {
                throw new ProductException("Product already exists.");
            }

            await db.SaveChangesAsync();

            return Obj.Id;
        }

        public static async Task<string> deleteProducts(int key)
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();

            var Obj = await db.Products.FirstOrDefaultAsync(o => o.Id == key);
            if (Obj == null) throw new ProductException("Product does not exist.");

            if (Obj.ProductStocks.Count > 0)
            {
                throw new ProductException("Remove the product recipe before deleting the product");
            }

            if (Obj.ProductItemProducts.Count > 0)
            {
                throw new ProductException("Remove the product recipe before deleting the product");
            }

            db.Products.Remove(Obj);
            await db.SaveChangesAsync();

            return "Product Deleted";
        }
    }
}
