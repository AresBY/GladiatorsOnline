using Gladiators.Data.Enums;

namespace Gladiators.Business.DTOs
{
    public class AchievementDto
    {
        public Guid Id { get; set; }
        public AchievementType Type { get; set; }
        public int Level { get; set; }

        public required string Description { get; set; }
    }
}
