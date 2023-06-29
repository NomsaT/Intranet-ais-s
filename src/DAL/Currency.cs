using System.Linq;

namespace DAL
{
    public static class Currency
    {
        public static IQueryable<DAL.DTO.Currency> getCurrency()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.Currencies
               .Select(p => new DAL.DTO.Currency
               {
                   Id = p.Id,
                   CurrencyName = p.CurrencyName,
                   CurrencyValue = p.CurrencyValue,
                   Iso = p.Iso
               });
            return source;
        }
    }
}
