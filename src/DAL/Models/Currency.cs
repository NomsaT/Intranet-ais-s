using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Iso { get; set; }
        public string CurrencyName { get; set; }
        public decimal? CurrencyValue { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
