using System;

namespace DAL.DTO
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ProjectStatusId { get; set; }
        public decimal Budget { get; set; }
    }
}
