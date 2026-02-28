using System.ComponentModel.DataAnnotations;

namespace Gladiators.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}