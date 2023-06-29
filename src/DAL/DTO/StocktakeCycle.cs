using System;
using System.Collections.Generic;

namespace DAL.DTO
{
    public class StocktakeCycle
    {
        public int Id { get; set; }
        public string StocktakeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool? Cycle { get; set; } = true;
    }
}