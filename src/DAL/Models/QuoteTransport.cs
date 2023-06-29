using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class QuoteTransport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int QuoteId { get; set; }

        public virtual Quote Quote { get; set; }
    }
}
