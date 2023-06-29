using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Service
    {
        public Service()
        {
            InvoiceServiceItems = new HashSet<InvoiceServiceItem>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int InternalOrderId { get; set; }
        public decimal Value { get; set; }
        public bool? VatAppl { get; set; }
        public bool? GrnAppl { get; set; }
        public int? GlcodeId { get; set; }
        public int DepartmentId { get; set; }
        public int? Quantity { get; set; }
        public string Code { get; set; }

        public virtual Department Department { get; set; }
        public virtual Glcode Glcode { get; set; }
        public virtual InternalOrder InternalOrder { get; set; }
        public virtual ICollection<InvoiceServiceItem> InvoiceServiceItems { get; set; }
    }
}
