using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class Quote
    {
        public int Id { get; set; }
        public int? QuoteStatusId { get; set; }
        public string QuoteStatusDisplay { get; set; }
        public int? RequestById { get; set; }
        public string RequestByFullName { get; set; }
        public int? PlacedById { get; set; }
        public string PlacedByFullName { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? ValidFor { get; set; }
        public int? DaysFrom { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? OnDelivery { get; set; }
        public int CustomerId { get; set; }
        public string CompanyName { get; set; }
        public List<QuoteItem> QuoteItems { get; set; }
        public List<QuoteTransport> QuoteTransports { get; set; }
        public List<QuoteRevision> QuoteRevisions { get; set; }
    }
}
