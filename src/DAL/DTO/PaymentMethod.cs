namespace DAL.DTO
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public bool? RequireBankingDetails { get; set; }
    }
}
