namespace DAL.DTO
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal ItemValue { get; set; }
        public decimal getTotal { get; set; }
        public int InvoiceId { get; set; }
        public int GrnQtyTotal { get; set; }
        public int? GlcodeId { get; set; }
        public int DepartmentId { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerProductName { get; set; }
        public string ProductName { get; set; }
        public string InternalProductName { get; set; }
        public int RequiredQuantity { get; set; }
        public int? GrnitemId { get; set; }
        public int ReceivedQuantity { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public bool? VatAppl { get; set; }
        public decimal InvoicePrice { get; set; }
    }
}
