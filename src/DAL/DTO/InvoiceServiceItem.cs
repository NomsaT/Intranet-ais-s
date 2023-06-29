namespace DAL.DTO
{
    public class InvoiceServiceItem
    {
         public int Id { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal ItemValue { get; set; }
        public decimal getTotal { get; set; }
        public int? GlcodeId { get; set; }
        public int InvoiceId { get; set; }
        public int ServiceId { get; set; }
        public bool? VatAppl { get; set; }
        public decimal InvoicePrice { get; set; }
        public int TotalUomReceived { get; set; }
        public int TotalUomOutstanding { get; set; }
        public int RequiredQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public int DepartmentId { get; set; }
        public string Code { get; set; }
    }
}
