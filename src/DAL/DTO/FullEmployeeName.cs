using System;

namespace DAL.DTO
{
    public class FullEmployeeName
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FullNamePosition { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime Birthday { get; set; }
    }
}
