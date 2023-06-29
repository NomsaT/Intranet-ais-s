namespace DAL.DTO
{
    public class InvoiceOnceOffItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int GrnQtyTotal { get; set; }
        public decimal ItemValue { get; set; }
        public int InvoiceId { get; set; }
        public int GrnonceOffItemId { get; set; }
        public bool? VatAppl { get; set; }
        public decimal? InvoicePrice { get; set; }
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public decimal getTotal { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public int TotalUomReceived { get; set; }
        public int TotalUomOutstanding { get; set; }
        public int RequiredQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public int? GlcodeId { get; set; }
        public string Code { get; set; }
    }
}
