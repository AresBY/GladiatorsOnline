namespace Gladiators.Business.DTOs
{
    public class GladiatorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int PortraitID { get; set; }

        // public List<Achievement> Achievements { get; set; } = new();
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intuition { get; set; }
        public int Stamina { get; set; }
        public int Damage { get; set; }
        public int Dodge { get; set; }
        public int AntiDodge { get; set; }
        public int Critical { get; set; }
        public int AntiCritical { get; set; }
        public int HP { get; set; }
        public int Wins { get; set; }
    }
}
