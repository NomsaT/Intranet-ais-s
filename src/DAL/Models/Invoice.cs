using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
            InvoiceOnceOffItems = new HashSet<InvoiceOnceOffItem>();
            InvoiceServiceItems = new HashSet<InvoiceServiceItem>();
        }

        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Total { get; set; }
        public int InternalOrderId { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual InternalOrder InternalOrder { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        public virtual ICollection<InvoiceOnceOffItem> InvoiceOnceOffItems { get; set; }
        public virtual ICollection<InvoiceServiceItem> InvoiceServiceItems { get; set; }
    }
}
