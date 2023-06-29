using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string Idnumber { get; set; }
        public string EmployeeNumber { get; set; }
        public string ContactNumber { get; set; }
        public DateTime Birthday { get; set; }
        public string IncomeTaxNumber { get; set; }
        public int RaceId { get; set; }
        public int GenderId { get; set; }
        public int HomeAddressId { get; set; }
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
        public int EmployeePositionId { get; set; }
        public int PaymentIntervalsId { get; set; }
        public int LawsId { get; set; }

        public virtual BankName BankName { get; set; }
        public virtual EmployeePosition EmployeePosition { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual Address HomeAddress { get; set; }
        public virtual Law Laws { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual PaymentInterval PaymentIntervals { get; set; }
        public virtual Race Race { get; set; }
        public virtual TypeEmployment TypeOfEmployment { get; set; }
        public virtual User User { get; set; }
    }
}
