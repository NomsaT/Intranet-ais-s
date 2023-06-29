using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class StocktakeCounter
    {
        public int? CycleCounter { get; set; }
        public int? UnScheduledCounter{ get; set; }
    }
}
