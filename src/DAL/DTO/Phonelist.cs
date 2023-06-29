namespace DAL.DTO
{
    public class Phonelist
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string WorkLandlineNumber { get; set; }
        public string PersonPosition { get; set; }
        public int? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public int? ContactId { get; set; }
        public string? CompanyName { get; set; }
    }
}
