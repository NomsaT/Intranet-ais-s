using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PaymentMethod
    {
        public PaymentMethod()
        {
            Customers = new HashSet<Customer>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool? RequireBankingDetails { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
