using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Glcode
    {
        public Glcode()
        {
            InternalOrderItems = new HashSet<InternalOrderItem>();
            OnceOffItems = new HashSet<OnceOffItem>();
            Services = new HashSet<Service>();
            VatStoreds = new HashSet<VatStored>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? AssignVat { get; set; }

        public virtual ICollection<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual ICollection<OnceOffItem> OnceOffItems { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<VatStored> VatStoreds { get; set; }
    }
}
