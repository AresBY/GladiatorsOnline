namespace Gladiators.Business.DTOs
{
    public class BaseSlaveDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intuition { get; set; }
        public int Stamina { get; set; }
    }

    public class PlayersSlaveDto : BaseSlaveDto
    {
        public Guid? OwnerId { get; set; }
    }

    public class MarketSlaveDto : BaseSlaveDto
    {
        public Guid? PlayerId { get; set; }
        public int Price { get; set; }
    }
}
