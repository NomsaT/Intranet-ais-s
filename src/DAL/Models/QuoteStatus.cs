using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class QuoteStatus
    {
        public QuoteStatus()
        {
            Quotes = new HashSet<Quote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Quote> Quotes { get; set; }
    }
}
