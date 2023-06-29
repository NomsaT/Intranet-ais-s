using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InvoiceServiceItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal ItemValue { get; set; }
        public int InvoiceId { get; set; }
        public int ServiceId { get; set; }
        public decimal InvoicePrice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Service Service { get; set; }
    }
}
