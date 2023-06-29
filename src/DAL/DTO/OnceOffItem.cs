using System;

namespace DAL.DTO
{
    public class OnceOffItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int InternalOrderId { get; set; }
        public decimal? Value { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int? Uomid { get; set; }
        public string UomName { get; set; }
        public int PackSize { get; set; }
        public bool? VatAppl { get; set; }
        public bool? GrnAppl { get; set; }
        public int? GlcodeId { get; set; }
        public string? GlFullName { get; set; }
        public DateTime? DateCreated { get; set; }
        public int TotalUnits { get; set; }
        public string Code { get; set; }
        public string SupplierCurrency { get; set; }
    }
}
