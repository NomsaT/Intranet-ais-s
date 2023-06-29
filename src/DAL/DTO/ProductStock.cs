namespace DAL.DTO
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal QtyPerSquareMeter { get; set; }
        public decimal SquaresUsed { get; set; }
        public string UomName { get; set; }
    }
}
