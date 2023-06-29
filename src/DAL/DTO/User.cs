using System;

namespace DAL.DTO
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastActivity { get; set; }
        public bool IsDisabled { get; set; }
        public DateTime? ResetDateTime { get; set; }
        public bool SendEmail { get; set; }
    }
}
