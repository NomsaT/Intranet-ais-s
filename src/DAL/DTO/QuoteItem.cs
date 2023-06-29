namespace DAL.DTO
{
    public class QuoteItem
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public decimal? Width { get; set; }
        public decimal? Length { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public int QuoteId { get; set; }
        public string ProductName { get; set; }
        public double m2Coil { get; set; }
        public double priceCoil { get; set; }
        public decimal Total { get; set; }

        public string FormattedAmount
        {
            get
            {
                return Price == null ? "null" : string.Format("{0:C}", Price.Value);
            }
        }
    }
}
