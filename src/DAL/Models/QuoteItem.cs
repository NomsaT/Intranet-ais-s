using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class QuoteItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int QuoteId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Quote Quote { get; set; }
    }
}
