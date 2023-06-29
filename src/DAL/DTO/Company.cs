namespace DAL.DTO
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VatNr { get; set; }
        public string RegNr { get; set; }
        public int AddressId { get; set; }
        public bool? MotherCompany { get; set; }
        public SupplierAddress Address { get; set; }
    }
}
