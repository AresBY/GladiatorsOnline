using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IPlayerSlaveService
    {
        Task DeleteAsync(Guid slaveId);
        Task<IEnumerable<PlayersSlave>> GetAllAsync(Guid playerId);
        Task<PlayersSlave> GetAsync(Guid id);
        Task UpdateAsync(PlayersSlave slave);
    }
}
