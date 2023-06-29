#nullable disable

namespace DAL.Models
{
    public partial class MonitorStockOrderApproval
    {
        public int Id { get; set; }
        public int InternalOrderItemId { get; set; }

        public virtual InternalOrderItem InternalOrderItem { get; set; }
    }
}
