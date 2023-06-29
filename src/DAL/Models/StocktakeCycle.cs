using System;
using System.Collections.Generic;

#nullable disable

namespace DAL.Models
{
    public partial class StocktakeCycle
    {
        public StocktakeCycle()
        {
            StocktakeReports = new HashSet<StocktakeReport>();
        }

        public int Id { get; set; }
        public string StocktakeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<StocktakeReport> StocktakeReports { get; set; }
    }
}
