using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Contacts = new HashSet<Contact>();
            Quotes = new HashSet<Quote>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPrefix { get; set; }
        public int PaymentMethodId { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public int PhysicalAddressId { get; set; }
        public int DeliveryAddressId { get; set; }
        public bool DifferentDelivery { get; set; }

        public virtual Address DeliveryAddress { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual Address PhysicalAddress { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
