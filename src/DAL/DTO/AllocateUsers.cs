namespace DAL.DTO
{
    public class AllocateUsers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string EmployeeNumber { get; set; }
        public decimal Percentage { get; set; }
        public bool IsDisabled { get; set; }
    }
}
