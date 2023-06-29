using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Contacts = new HashSet<Contact>();
            InternalOrders = new HashSet<InternalOrder>();
            PriceLookups = new HashSet<PriceLookup>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public decimal CreditLimit { get; set; }
        public string CreditTerms { get; set; }
        public decimal Discount { get; set; }
        public int AddressId { get; set; }
        public string Comment { get; set; }
        public int? BankNameId { get; set; }
        public string AccountNumber { get; set; }
        public string BranchCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public int CurrencyId { get; set; }

        public virtual Address Address { get; set; }
        public virtual BankName BankName { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<InternalOrder> InternalOrders { get; set; }
        public virtual ICollection<PriceLookup> PriceLookups { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
