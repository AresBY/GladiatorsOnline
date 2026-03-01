using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IPlayerSlaveRepository : IBaseRepository<PlayersSlave>
    {
        Task<List<PlayersSlave>> GetAllAsync(Guid playerId);
    }
}
