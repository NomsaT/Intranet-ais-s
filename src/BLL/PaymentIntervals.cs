using System.Linq;

namespace BLL
{
    public static class PaymentIntervals
    {
        public static IQueryable<DAL.DTO.PaymentInterval> getPaymentIntervals()
        {
            return DAL.PaymentIntervals.getPaymentIntervals();
        }
    }
}
