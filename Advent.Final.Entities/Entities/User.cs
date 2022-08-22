
using System.ComponentModel.DataAnnotations;

namespace Advent.Final.Entities.Entities
{
    public class User : Person
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        public string? Password { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime LastLogIn { get; set; }
        public DateTime? LastLogOut { get; set; }
        public string? Token { get; set; }
        [Required]
        public string Status { get; set; }
    }
}
