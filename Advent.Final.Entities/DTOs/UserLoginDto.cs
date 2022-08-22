
using Advent.Final.Entities.Entities;

namespace Advent.Final.Entities.DTOs
{
    public class UserLoginDto
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
