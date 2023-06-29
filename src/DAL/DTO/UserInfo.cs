using System.Collections.Generic;

namespace DAL.DTO
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public List<int> Permissions { get; set; }
    }
}
