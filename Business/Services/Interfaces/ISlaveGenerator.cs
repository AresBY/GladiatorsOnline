using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface ISlaveGenerator
    {
        List<MarketSlave> CreateSlaves(int count, Guid playerId);
    }
}
