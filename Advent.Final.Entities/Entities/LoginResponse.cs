
namespace Advent.Final.Entities.DTOs
{
    public class LoginResponse
    {
        public string User { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
