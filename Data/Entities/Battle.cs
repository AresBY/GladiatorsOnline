namespace Gladiators.Data.Entities
{
    public class Battle : BaseEntity
    {
        public FighterInformation FirstFighter { get; set; }

        public FighterInformation SecondFighter { get; set; }
        public Battle(FighterInformation firstFighter, FighterInformation secondFighter)
        {
            FirstFighter = firstFighter;
            SecondFighter = secondFighter;
        }
        public List<AttackResult> BattleRounds { get; set; } = new List<AttackResult>();
    }
    public class FighterInformation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }

        public bool IsWinner { get; set; }
    }
    public struct AttackResult
    {
        public int DamageDealt { get; set; }
        public bool Missed { get; set; }
        public bool Critical { get; set; }
    }
}
