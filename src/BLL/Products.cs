using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Products
    {
        public static IQueryable<DAL.DTO.Product> getProducts()
        {
            return DAL.Products.getProducts();
        }

        public static int addProducts(string values)
        {
            return DAL.Products.addProducts(values);
        }
        public static Task<int> editProducts(int key, string values)
        {
            return DAL.Products.editProducts(key, values);
        }

        public static Task<string> deleteProducts(int key)
        {
            return DAL.Products.deleteProducts(key);
        }
    }
}
