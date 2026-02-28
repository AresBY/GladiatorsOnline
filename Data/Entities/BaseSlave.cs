namespace Gladiators.Data.Entities
{
    public class BaseSlave : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int PortraitID { get; set; }

        // public List<Achievement> Achievements { get; set; } = new();
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intuition { get; set; }
        public int Stamina { get; set; }
        public int Wins { get; set; }
    }
}
