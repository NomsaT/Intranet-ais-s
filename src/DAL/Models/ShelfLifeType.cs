using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class ShelfLifeType
    {
        public ShelfLifeType()
        {
            Stocks = new HashSet<Stock>();
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public int Days { get; set; }

        public virtual ICollection<Stock> Stocks { get; set; }
    }
}
