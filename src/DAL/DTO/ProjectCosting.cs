using System;

namespace DAL.DTO
{
    public class ProjectCosting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectStatusId { get; set; }
        public decimal Budget { get; set; }
        public decimal AllInternalOrderTotals { get; set; }
        public decimal Difference { get; set; }
        public int DaysLeft { get; set; }
    }
}
