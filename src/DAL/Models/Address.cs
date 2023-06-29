using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Address
    {
        public Address()
        {
            Companies = new HashSet<Company>();
            CustomerDeliveryAddresses = new HashSet<Customer>();
            CustomerPhysicalAddresses = new HashSet<Customer>();
            PlantLocations = new HashSet<PlantLocation>();
            Suppliers = new HashSet<Supplier>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Customer> CustomerDeliveryAddresses { get; set; }
        public virtual ICollection<Customer> CustomerPhysicalAddresses { get; set; }
        public virtual ICollection<PlantLocation> PlantLocations { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
