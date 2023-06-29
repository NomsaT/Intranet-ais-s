using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class OnceOffItem
    {
        public OnceOffItem()
        {
            GrnonceOffItems = new HashSet<GrnonceOffItem>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public int InternalOrderId { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
        public int DepartmentId { get; set; }
        public int? Uomid { get; set; }
        public int PackSize { get; set; }
        public bool? VatAppl { get; set; }
        public bool? GrnAppl { get; set; }
        public int? GlcodeId { get; set; }
        public string Code { get; set; }

        public virtual Department Department { get; set; }
        public virtual Glcode Glcode { get; set; }
        public virtual InternalOrder InternalOrder { get; set; }
        public virtual UnitOfMeasurement Uom { get; set; }
        public virtual ICollection<GrnonceOffItem> GrnonceOffItems { get; set; }
    }
}
