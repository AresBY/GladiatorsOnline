namespace Gladiators.Business.DTOs
{
    public class MakeChampionRequest
    {
        public Guid PlayerId { get; set; }
        public Guid ChampionId { get; set; }
    }
}
