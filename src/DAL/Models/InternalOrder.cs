using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class InternalOrder
    {
        public InternalOrder()
        {
            Grns = new HashSet<Grn>();
            InternalOrderItems = new HashSet<InternalOrderItem>();
            Invoices = new HashSet<Invoice>();
            LatestGrns = new HashSet<LatestGrn>();
            OnceOffItems = new HashSet<OnceOffItem>();
            Services = new HashSet<Service>();
        }

        public int Id { get; set; }
        public int RequestedById { get; set; }
        public int PlacedById { get; set; }
        public string QuotationNumber { get; set; }
        public int? SupplierId { get; set; }
        public int? ApproveById { get; set; }
        public string Comment { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal? Total { get; set; }
        public int StatusId { get; set; }
        public DateTime? DateApproved { get; set; }
        public string InternalComment { get; set; }
        public int? PlantLocationId { get; set; }
        public string SupplierComment { get; set; }
        public decimal? Vat { get; set; }
        public int? ProjectId { get; set; }
        public int? CompanyId { get; set; }

        public virtual User ApproveBy { get; set; }
        public virtual Company Company { get; set; }
        public virtual User PlacedBy { get; set; }
        public virtual PlantLocation PlantLocation { get; set; }
        public virtual Project Project { get; set; }
        public virtual User RequestedBy { get; set; }
        public virtual InternalOrderStatus Status { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<Grn> Grns { get; set; }
        public virtual ICollection<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<LatestGrn> LatestGrns { get; set; }
        public virtual ICollection<OnceOffItem> OnceOffItems { get; set; }
        public virtual ICollection<Service> Services { get; set; }
    }
}
