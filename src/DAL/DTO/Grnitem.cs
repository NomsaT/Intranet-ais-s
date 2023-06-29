namespace DAL.DTO
{
    public class Grnitem
    {
        public int Id { get; set; }
        public int StockId { get; set; }
        public int Quantity { get; set; }
        public int InternalOrderQuantity { get; set; }
        public int GrnId { get; set; }
        public int InternalOrderItemId { get; set; }
        public int? StoreLocationId { get; set; }
        public string ManufacturerCode { get; set; }
        public string ManufacturerProductName { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public int TotalUomReceived { get; set; }
        public int TotalUomOutstanding { get; set; }
        public int RequiredQuantity { get; set; }
        public int ReceivedQuantity { get; set; }
        public string Comment { get; set; }
    }
}
