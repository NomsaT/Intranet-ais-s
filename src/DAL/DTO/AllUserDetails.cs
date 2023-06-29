using System;

namespace DAL.DTO
{
    public class AllUserDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastActivity { get; set; }
        public bool IsDisabled { get; set; }
        public string Idnumber { get; set; }
        public string EmployeeNumber { get; set; }
        public string ContactNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string IncomeTaxNumber { get; set; }
        public DateTime? ResetDateTime { get; set; }
        public int RaceId { get; set; }
        public int GenderId { get; set; }
        public int HomeAddressId { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string Suburb { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public int CountryId { get; set; }
        public string NextOfKinName { get; set; }
        public string NextOfKinContactNumber { get; set; }
        public int BankNameId { get; set; }
        public string AccountNumber { get; set; }
        public string BranchCode { get; set; }
        public decimal BaseSalaryPerMonth { get; set; }
        public decimal HourlyRate { get; set; }
        public string HighestQualification { get; set; }
        public int MaritalStatusId { get; set; }
        public int NumberOfDependants { get; set; }
        public int TypeOfEmploymentId { get; set; }
        public string Email { get; set; }
        public DateTime StartDate { get; set; }
        public int UserId { get; set; }
        public int UserDetailsId { get; set; }
        public int EmployeePositionId { get; set; }
        public string Position { get; set; }
        public string PositionDescription { get; set; }
        public int PaymentIntervalsId { get; set; }
        public string Intervals { get; set; }
        public string IntervalsDescription { get; set; }
        public int LawsId { get; set; }
        public string Law1 { get; set; }
        public string LawDescription { get; set; }
        public int PlantLocationId { get; set; }
    }
}
