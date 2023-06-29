using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Grnitem
    {
        public Grnitem()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public int GrnId { get; set; }
        public int InternalOrderItemId { get; set; }
        public int? StoreLocationId { get; set; }
        public string Comment { get; set; }

        public virtual Grn Grn { get; set; }
        public virtual InternalOrderItem InternalOrderItem { get; set; }
        public virtual Store StoreLocation { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
