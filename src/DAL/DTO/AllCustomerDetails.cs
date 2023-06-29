using System.Collections.Generic;

namespace DAL.DTO
{
    public class AllCustomerDetails
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyPrefix { get; set; }
        public int PaymentMethodId { get; set; }
        public string AccountNumber { get; set; }
        public string Description { get; set; }
        public int PhysicalAddressId { get; set; }
        public string PhysicalStreetAddress1 { get; set; }
        public string PhysicalStreetAddress2 { get; set; }
        public string PhysicalSuburb { get; set; }
        public string PhysicalCity { get; set; }
        public string PhysicalPostCode { get; set; }
        public string PhysicalCountry { get; set; }
        public int DeliveryAddressId { get; set; }
        public string DeliveryStreetAddress1 { get; set; }
        public string DeliveryStreetAddress2 { get; set; }
        public string DeliverySuburb { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryPostCode { get; set; }
        public string DeliveryCountry { get; set; }
        public bool DifferentDelivery { get; set; }
        public CustomerAddress PhysicalAddress { get; set; }
        public CustomerAddress DeliveryAddress { get; set; }
        public List<CustomerContact> Contacts { get; set; }

        public AllCustomerDetails()
        {
            PhysicalAddress = new CustomerAddress();
            DeliveryAddress = new CustomerAddress();
            Contacts = new List<CustomerContact>();
        }

    }
}
