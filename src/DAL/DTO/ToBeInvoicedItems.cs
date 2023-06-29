using System.Collections.Generic;

namespace DAL.DTO
{
    public class ToBeInvoicedItems
    {
        public List<int> ListedItem { get; set; }
        public List<int> OnceOffItem { get; set; }
        public List<int> Service { get; set; }
        public int InternalOrderId { get; set; }
        public int action { get; set; }
    }
}
