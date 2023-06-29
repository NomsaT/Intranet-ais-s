using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class IncreaseType
    {
        public IncreaseType()
        {
            PriceIncreases = new HashSet<PriceIncrease>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<PriceIncrease> PriceIncreases { get; set; }
    }
}
