namespace Gladiators.Data.Entities
{
    public class Battle : BaseEntity
    {
        public Guid FirstSlaveID { get; set; }
        public string FirstSlaveName { get; set; }
        public Guid SecondSlaveID { get; set; }
        public string SecondSlaveName { get; set; }
        public Guid WinnerId { get; set; }
        public Guid LoserId { get; set; }
        public Battle(Guid firstSlaveID, Guid secondSlaveID, string firstSlaveName, string secondSlaveName)
        {
            Id = Guid.NewGuid();
            FirstSlaveID = firstSlaveID;
            SecondSlaveID = secondSlaveID;
            FirstSlaveName = firstSlaveName;
            SecondSlaveName = secondSlaveName;
        }
        public List<AttackResult> BattleRounds { get; set; } = new List<AttackResult>();
    }
    public struct AttackResult
    {
        public int DamageDealt { get; set; }
        public bool Missed { get; set; }
        public bool Critical { get; set; }
    }
}
