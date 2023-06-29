using System.Linq;
using System.Threading.Tasks;

namespace BLL
{
    public static class BankNames
    {
        public static IQueryable<DAL.DTO.BankName> getBankNames()
        {
            return DAL.BankNames.getBankNames();
        }

        public static int addBankNames(string values)
        {
            return DAL.BankNames.addBankNames(values);
        }
        public static Task<int> editBankNames(int key, string values)
        {
            return DAL.BankNames.editBankNames(key, values);
        }

        public static Task<string> deleteBankNames(int key)
        {
            return DAL.BankNames.deleteBankNames(key);
        }
    }
}
