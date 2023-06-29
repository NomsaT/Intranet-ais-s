namespace DAL.DTO
{
    public class Currency
    {
        public int Id { get; set; }
        public string Iso { get; set; }
        public string CurrencyName { get; set; }
        public decimal? CurrencyValue { get; set; }
    }
}
