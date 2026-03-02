namespace Gladiators.Data.Entities
{
    public class AchievementDefinition
    {
        public string Type { get; set; } = null!;
        public int Level { get; set; }
        public string Criterion { get; set; } = null!;
        public int BonusPercent { get; set; }
        public string Description { get; set; } = null!;
    }
}
