using System.ComponentModel.DataAnnotations;

namespace Gladiators.Data.Entities
{
    public class User : BaseEntity
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;


        // Навигации
        public ICollection<PlayersSlave> PlayerSlaves { get; set; } = new List<PlayersSlave>();
        public ICollection<MarketSlave> MarketSlaves { get; set; } = new List<MarketSlave>();
    }
}