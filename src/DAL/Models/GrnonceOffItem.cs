using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class GrnonceOffItem
    {
        public GrnonceOffItem()
        {
            InvoiceOnceOffItems = new HashSet<InvoiceOnceOffItem>();
        }

        public int Id { get; set; }
        public int Quantity { get; set; }
        public int GrnId { get; set; }
        public int OnceOffItemsId { get; set; }
        public string Comment { get; set; }

        public virtual Grn Grn { get; set; }
        public virtual OnceOffItem OnceOffItems { get; set; }
        public virtual ICollection<InvoiceOnceOffItem> InvoiceOnceOffItems { get; set; }
    }
}
