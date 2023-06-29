using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class User
    {
        public User()
        {
            DepartmentManagers = new HashSet<DepartmentManager>();
            DepartmentUsers = new HashSet<DepartmentUser>();
            GrnCapturers = new HashSet<Grn>();
            GrnEditors = new HashSet<Grn>();
            InternalOrderApproveBies = new HashSet<InternalOrder>();
            InternalOrderPlacedBies = new HashSet<InternalOrder>();
            InternalOrderRequestedBies = new HashSet<InternalOrder>();
            QuotePlacedBies = new HashSet<Quote>();
            QuoteRequestBies = new HashSet<Quote>();
            Stocktakes = new HashSet<Stocktake>();
            UserDetails = new HashSet<UserDetail>();
            UserPermissions = new HashSet<UserPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastActivity { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? ResetDateTime { get; set; }
        public bool SendEmail { get; set; }

        public virtual ICollection<DepartmentManager> DepartmentManagers { get; set; }
        public virtual ICollection<DepartmentUser> DepartmentUsers { get; set; }
        public virtual ICollection<Grn> GrnCapturers { get; set; }
        public virtual ICollection<Grn> GrnEditors { get; set; }
        public virtual ICollection<InternalOrder> InternalOrderApproveBies { get; set; }
        public virtual ICollection<InternalOrder> InternalOrderPlacedBies { get; set; }
        public virtual ICollection<InternalOrder> InternalOrderRequestedBies { get; set; }
        public virtual ICollection<Quote> QuotePlacedBies { get; set; }
        public virtual ICollection<Quote> QuoteRequestBies { get; set; }
        public virtual ICollection<Stocktake> Stocktakes { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
