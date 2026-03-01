namespace Gladiators.Data.Entities
{
    public class Fighter : BaseEntity
    {
        public required string Name { get; set; }
        public int Damage { get; set; }
        public int Dodge { get; set; }
        public int AntiDodge { get; set; }
        public int Critical { get; set; }
        public int AntiCritical { get; set; }
        public int HP { get; set; }
        public int HPMax { get; set; }
        public int Wins { get; set; }
    }
}
