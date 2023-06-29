using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class PurchaseOrder
    {

        //InternalOrder
        public int Id { get; set; }
        public string RequestByFullName { get; set; }
        public int PONumber { get; set; }

        public decimal VAT { get;set;}
        public string QuotationNumber { get; set; }
        public string CompanyInternalName { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal Total { get; set; }
        //public decimal VAT { get { return Total * 15 / 100; } }
        //public decimal TotalAndVAT { get { return Total + VAT; } }
        public decimal TotalAndVAT { get{ return Total + VAT; } }

        public List<InternalOrderItem> InternalOrderItems { get; set; }
        public List<OnceOffItem> onceOffItems { get; set; }
        public List<Service> services { get; set; }


        //Supplier
        public string CompanyName { get; set; }
        public string SupplierSuburb { get; set; }
        public string SupplierPhysicalStreetAddress1 { get; set; }
        public string SupplierPhysicalCity { get; set; }
        public string SupplierPostCode { get; set; }


        //plantlocation
        public string PlantLocationStreetAddress1 { get; set; }
        public string PlantLocationPhysicalCity { get; set; }
        public string PlantLocationSuburb { get; set; }
        public string PlantLocationPostalCode { get; set; }

    }
}
