
using System.ComponentModel.DataAnnotations;

namespace Advent.Final.Entities.Entities
{
    public class Person
    {
        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
    }
}
