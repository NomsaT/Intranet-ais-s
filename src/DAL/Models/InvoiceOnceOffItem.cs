using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InvoiceOnceOffItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal ItemValue { get; set; }
        public int InvoiceId { get; set; }
        public int GrnonceOffItemId { get; set; }
        public decimal? InvoicePrice { get; set; }

        public virtual GrnonceOffItem GrnonceOffItem { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
