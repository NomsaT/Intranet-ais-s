using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class Company
    {
        public static IQueryable<DAL.DTO.Company> getCompany()
        {
            return DAL.Company.getCompany();
        }
        public static int addCompany(string values)
        {
            return DAL.Company.addCompany(values);
        }

        public static Task<int> editCompany(int key, string values)
        {
            return DAL.Company.editCompany(key, values);
        }

        public static Task<int> deleteCompany(int key)
        {
            return DAL.Company.deleteCompany(key);
        }
    }
}
