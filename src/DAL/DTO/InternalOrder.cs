using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class InternalOrder
    {
        public int Id { get; set; }
        public int RequestedById { get; set; }
        public string RequestByFullName { get; set; }
        public int PlacedById { get; set; }
        public string PlacedByFullName { get; set; }
        public string QuotationNumber { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierFullName { get; set; } 
        public int? ApproveById { get; set; }
        public string ApproveByFullName { get; set; }
        public string Comment { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string DeliveryMethod { get; set; }        
        public decimal? Total { get; set; }
        public decimal TotalDisplay { get; set; }
        public int StatusId { get; set; }
        public string StatusDisplay { get; set; }
        public DateTime? DateApproved { get; set; }
        public string InternalComment { get; set; }
        public int? PlantLocationId { get; set; }
        public string PlantLocationName { get; set; }        
        public string SupplierComment { get; set; }
        public string SupplierCurrency { get; set; }
        public string ApprovedIOSupplierName { get; set; }
        public decimal? Vat { get; set; }
        public List<InternalOrderItem> internalOrderItems { get; set; }
        public List<OnceOffItem> onceOffItems { get; set; }
        public List<Service> services { get; set; }
        public string Username { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int? CompanyId { get; set; }
        public string CompanyName { get; set; }
    }
}
