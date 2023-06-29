using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class PriceIncrease
    {
        public int Id { get; set; }
        public decimal Increase { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public int IncreaseTypeId { get; set; }
        public int PriceLookUpId { get; set; }
        public string IncreaseOrigin { get; set; }

        public virtual IncreaseType IncreaseType { get; set; }
        public virtual PriceLookup PriceLookUp { get; set; }
    }
}
