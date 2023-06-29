namespace DAL.DTO
{
    public class InternalOrderItem
    {
        public int Id { get; set; }
        public int InternalOrderId { get; set; }
        public int StockId { get; set; }
        public string StockName { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string InternalProductName { get; set; }
        public decimal OriginalValue { get; set; }
        public decimal Value { get; set; }
        public int Quantity { get; set; }
        public int? GlcodeId { get; set; }
        public string? GlFullName { get; set; }
        public decimal Total { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int Uomid { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public bool? VatAppl { get; set; }
        public bool? GrnAppl { get; set; }
        public int TotalUnits { get; set; }
        public decimal? UomPrice { get; set; }
    }
}
