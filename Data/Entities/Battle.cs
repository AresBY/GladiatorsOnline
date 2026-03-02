namespace Gladiators.Data.Entities
{
    public class Battle : BaseEntity
    {
        public Guid FirstSlaveID { get; set; }
        public string FirstSlaveName { get; set; }
        public int FirstSlaveMaxHP { get; set; }

        public Guid SecondSlaveID { get; set; }
        public string SecondSlaveName { get; set; }
        public int SecondSlaveMaxHP { get; set; }

        public Guid WinnerId { get; set; }
        public Guid LoserId { get; set; }
        public Battle(Guid firstSlaveID, string firstSlaveName, int firstSlaveMaxHP,
            Guid secondSlaveID, string secondSlaveName, int secondSlaveMaxHP)
        {
            Id = Guid.NewGuid();

            FirstSlaveID = firstSlaveID;
            FirstSlaveName = firstSlaveName;
            FirstSlaveMaxHP = firstSlaveMaxHP;

            SecondSlaveID = secondSlaveID;
            SecondSlaveName = secondSlaveName;
            SecondSlaveMaxHP = secondSlaveMaxHP;
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
