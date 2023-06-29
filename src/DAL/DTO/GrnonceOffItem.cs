namespace DAL.DTO
{
    public class GrnonceOffItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int InternalOrderQuantity { get; set; }
        public int GrnId { get; set; }
        public int OnceOffItemsId { get; set; }
        public string Description { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public bool? GrnAppl { get; set; }
        public int TotalUomReceived { get; set; }
        public int TotalUomOutstanding { get; set; }
        public int RequiredQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public string Code { get; set; }
        public string Comment { get; set; }
    }
}
