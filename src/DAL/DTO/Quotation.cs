using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class Quotation
    {

        //Quotes
        public int Id { get; set; }
        public string RequestByFullName { get; set; }
        public string PlacedByFullName { get; set; }
        public int QuoteNo { get; set; }
        public decimal Total { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddressLine1 { get; set; }
        public string CustomerAddressLine2 { get; set; }
        public string CustomerSuburb { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerPostalCode { get; set; }
        public string CustomerTelephone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPerson { get; set; }
        public decimal VAT { get; set; }
        public int vatPerc { get; set; }
        public decimal TotalAndVAT { get; set; }
        public DateTime DateCreated { get; set; }
        public List<QuoteItem> QuoteItems { get; set; }
        public List<QuoteTransport> QuoteTransports { get; set; }
        public List<QuoteRevision> QuoteRevisions { get; set; }

    }
}
