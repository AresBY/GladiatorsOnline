namespace Gladiators.Data.Entities
{
    public class MarketSlave : BaseSlave
    {
        public Guid? PlayerId { get; set; }
        public int Price { get; set; }
    }
}
