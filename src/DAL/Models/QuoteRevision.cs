using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class QuoteRevision
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public int RevisionNr { get; set; }

        public virtual Quote Quote { get; set; }
    }
}
