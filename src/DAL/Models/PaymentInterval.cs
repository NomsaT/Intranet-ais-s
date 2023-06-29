using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PaymentInterval
    {
        public PaymentInterval()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string Intervals { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
