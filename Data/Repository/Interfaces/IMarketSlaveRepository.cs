using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IMarketSlaveRepository : IBaseRepository<MarketSlave>
    {
        Task<List<MarketSlave>> GetAllAsync(Guid playerId);
        Task<MarketSlave?> GetAsync(Guid slaveId);
        Task UpdateMarketSlavesAsync(List<MarketSlave> newSlaves);
    }
}
