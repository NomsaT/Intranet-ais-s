using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class BankName
    {
        public BankName()
        {
            Suppliers = new HashSet<Supplier>();
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string BankName1 { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
