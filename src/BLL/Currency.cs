using System.Linq;

namespace BLL
{
    public static class Currency
    {
        public static IQueryable<DAL.DTO.Currency> getCurrency()
        {
            return DAL.Currency.getCurrency();
        }
    }
}
