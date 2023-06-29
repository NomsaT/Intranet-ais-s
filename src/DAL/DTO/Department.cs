namespace DAL.DTO
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string AbbAndName { get; set; }
        public string Color { get; set; }
        public int CostTypeId { get; set; }
        public bool? GeneralPurchasing { get; set; }        
    }
}
