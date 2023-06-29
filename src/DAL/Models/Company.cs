using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Company
    {
        public Company()
        {
            InternalOrders = new HashSet<InternalOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string VatNr { get; set; }
        public string RegNr { get; set; }
        public int AddressId { get; set; }
        public bool? MotherCompany { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<InternalOrder> InternalOrders { get; set; }
    }
}
