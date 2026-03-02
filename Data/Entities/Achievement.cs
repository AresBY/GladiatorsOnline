using Gladiators.Data.Enums;

namespace Gladiators.Data.Entities
{
    public class Achievement : BaseEntity
    {
        public Guid PlayerSlaveId { get; set; }
        public AchievementType Type { get; set; }
        public int Level { get; set; }

        public PlayersSlave PlayersSlave { get; set; } = default!;

    }
}
