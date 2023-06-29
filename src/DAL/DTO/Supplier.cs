using System.Collections.Generic;

namespace DAL.DTO
{
    public class Supplier
    {
        public int Id { get; set; }        
        public string CompanyName { get; set; }
        public string SupplierFullName { get; set; }
        public decimal CreditLimit { get; set; }
        public string CreditTerms { get; set; }
        public decimal Discount { get; set; }
        public string Comment { get; set; }
        public int? BankNameId { get; set; }
        public string AccountNumber { get; set; }
        public string BranchCode { get; set; }
        public int? PaymentMethodId { get; set; }
        public int? LocationTypeId { get; set; }
        public int AddressId { get; set; }
        public int CurrencyId { get; set; }
        public int SupplierCategoryId { get; set; }
        public List<SupplierContact> Contacts { get; set; }
        public SupplierAddress Address { get; set; }

        public Supplier()
        {
            Address = new SupplierAddress();
            Contacts = new List<SupplierContact>();
        }
    }
}
