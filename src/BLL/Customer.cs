using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Customer
    {
        public static IQueryable<DAL.DTO.AllCustomerDetails> getCustomers()
        {
            return DAL.Customer.getCustomers();
        }

        public static Task<int> addCustomer(string values)
        {
            return DAL.Customer.addCustomer(values);
        }

        public static Task<int> updateCustomer(int paCustomerID, string paValues)
        {
            return DAL.Customer.updateCustomer(paCustomerID, paValues);
        }

        public static Task<string> deleteCustomer(int paCustomerID)
        {
            return DAL.Customer.deleteCustomer(paCustomerID);
        }
    }
}
