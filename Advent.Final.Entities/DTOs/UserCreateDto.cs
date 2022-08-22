using Advent.Final.Entities.Entities;

namespace Advent.Final.Entities.DTOs
{
    public class UserCreateDto: Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime LastLogOut { get; set; }
        public string Status { get; set; }
    }
}
