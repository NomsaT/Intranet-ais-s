namespace DAL.DTO
{
    public class TransferStockDepartment
    {
        public int StockId { get; set; }
        public int Id { get; set; }
        public decimal PackQuantityMove { get; set; }
        public int OriginDepartment { get; set; }
        public int NewDepartment { get; set; }
        public string quantityOption { get; set; }
        public int uomid { get; set; }

    }
}
