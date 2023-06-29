namespace DAL.DTO
{
    public class Contact
    {
        public int customerId { get; set; } = 0;
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string PersonPosition { get; set; }
        public string ContactNumber { get; set; }
        public string ContactEmail { get; set; }
        public string WorkLandlineNumber { get; set; }
        public int? SupplierId { get; set; }
        public int? ContactId { get; set; }
    }
}
