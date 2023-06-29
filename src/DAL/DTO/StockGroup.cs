namespace DAL.DTO
{
    public class StockGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? MinThreshold { get; set; }
    }
}
