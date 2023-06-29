namespace DAL.DTO
{
    public class QuoteTransport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int QuoteId { get; set; }
        public decimal Total { get; set; }

        public string FormattedPrice
        {
            get
            {
                return Price == null ? "null" : string.Format("{0:C}", Price.Value);
            }
        }
    }
}
