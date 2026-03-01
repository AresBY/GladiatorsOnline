namespace Gladiators.Data.Entities
{
    public class PlayersSlave : BaseSlave
    {
        public Guid? OwnerId { get; set; }

        public User Owner { get; set; } = default!;
    }
}
