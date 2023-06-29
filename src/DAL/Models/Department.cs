using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Department
    {
        public Department()
        {
            DepartmentManagers = new HashSet<DepartmentManager>();
            DepartmentUsers = new HashSet<DepartmentUser>();
            InternalOrderItems = new HashSet<InternalOrderItem>();
            OnceOffItems = new HashSet<OnceOffItem>();
            Products = new HashSet<Product>();
            Services = new HashSet<Service>();
            StockQuantities = new HashSet<StockQuantity>();
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public string Color { get; set; }
        public int CostTypeId { get; set; }
        public bool? GeneralPurchasing { get; set; }

        public virtual CostType CostType { get; set; }
        public virtual ICollection<DepartmentManager> DepartmentManagers { get; set; }
        public virtual ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public virtual ICollection<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual ICollection<OnceOffItem> OnceOffItems { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Service> Services { get; set; }
        public virtual ICollection<StockQuantity> StockQuantities { get; set; }
        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
