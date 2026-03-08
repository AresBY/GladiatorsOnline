namespace Gladiators.Data.Entities
{
    public class PlayersSlave : BaseSlave
    {
        public Guid? OwnerId { get; set; }

        public User Owner { get; set; } = default!;
        public int RemainingStatBoosts { get; set; }

        public ICollection<Achievement> Achievements { get; set; } = new List<Achievement>();
    }
}
