namespace Gladiators.Business.DTOs
{
    public class FighterDetailDto
    {
        public Guid Id { get; set; }
        public int PortraitID { get; set; }
        public string Name { get; set; } = null!;
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intuition { get; set; }
        public int Stamina { get; set; }
        public int Weight { get; set; }
        public int Wins { get; set; }

        public ICollection<AchievementDto> Achievements { get; set; } = new List<AchievementDto>();

        public int Damage { get; set; }
        public int Dodge { get; set; }
        public int AntiDodge { get; set; }
        public int Critical { get; set; }
        public int CriticalPower { get; set; }
        public int AntiCritical { get; set; }
        public int HP { get; set; }
        public int HPMax { get; set; }


        public int RemainingStatBoosts { get; set; }
    }
}
