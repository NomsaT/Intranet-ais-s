using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class UnitOfMeasurement
    {
        public UnitOfMeasurement()
        {
            InternalOrderItems = new HashSet<InternalOrderItem>();
            OnceOffItems = new HashSet<OnceOffItem>();
            StockSecondaryUoms = new HashSet<Stock>();
            StockUoms = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<InternalOrderItem> InternalOrderItems { get; set; }
        public virtual ICollection<OnceOffItem> OnceOffItems { get; set; }
        public virtual ICollection<Stock> StockSecondaryUoms { get; set; }
        public virtual ICollection<Stock> StockUoms { get; set; }
    }
}
