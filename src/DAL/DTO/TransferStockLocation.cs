namespace DAL.DTO
{
    public class TransferStockLocation
    {
        public int StockId { get; set; }
        public int Id { get; set; }
        public decimal PackQuantityMove { get; set; }
        public int originLocation { get; set; }
        public int originStore { get; set; }
        public int newLocation { get; set; }
        public int newStore { get; set; }
        public string quantityOption { get; set; }
        public int uomid { get; set; }
    }
}
