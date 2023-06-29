using System.Linq;

namespace DAL
{
    public static class PaymentIntervals
    {
        public static IQueryable<DAL.DTO.PaymentInterval> getPaymentIntervals()
        {
            DAL.Models.AISContext db = new DAL.Models.AISContext();
            var source = db.PaymentIntervals
               .Select(p => new DAL.DTO.PaymentInterval
               {
                   Id = p.Id,
                   Intervals = p.Intervals,
                   Description = p.Description

               });
            return source;
        }
    }
}
