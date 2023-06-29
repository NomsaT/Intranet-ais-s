namespace DAL.DTO
{
    public class Recipe
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int StockComponentId { get; set; }
        public decimal Quantity { get; set; }
        public string UomName { get; set; }
    }
}
