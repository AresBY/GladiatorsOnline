using Gladiators.Business.DTOs;
using Gladiators.Data.Entities;
using Gladiators.Data.Enums;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IPlayerSlaveService
    {
        Task DeleteAsync(Guid slaveId);
        Task<IEnumerable<PlayersSlaveDto>> GetAllAsync(Guid playerId);
        Task<PlayersSlave> GetAsync(Guid id);
        Task<FighterDetailDto> GetDetailAsync(Guid id);
        Task UpdateAsync(PlayersSlave slave);
        Task AddStatsAsync(Guid playerSlaveId, StatType statType, int amount);
    }
}
