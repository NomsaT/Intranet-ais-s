using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Quote
    {
        public Quote()
        {
            QuoteItems = new HashSet<QuoteItem>();
            QuoteRevisions = new HashSet<QuoteRevision>();
            QuoteTransports = new HashSet<QuoteTransport>();
        }

        public int Id { get; set; }
        public int? QuoteStatusId { get; set; }
        public int? RequestById { get; set; }
        public int? PlacedById { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public int? ValidFor { get; set; }
        public int? DaysFrom { get; set; }
        public decimal? OnOrder { get; set; }
        public decimal? OnDelivery { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User PlacedBy { get; set; }
        public virtual QuoteStatus QuoteStatus { get; set; }
        public virtual User RequestBy { get; set; }
        public virtual ICollection<QuoteItem> QuoteItems { get; set; }
        public virtual ICollection<QuoteRevision> QuoteRevisions { get; set; }
        public virtual ICollection<QuoteTransport> QuoteTransports { get; set; }
    }
}
