using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StocktakeCounter
    {
        public int Id { get; set; }
        public int? CycleCounter { get; set; }
        public int? UnScheduledCounter { get; set; }
    }
}
