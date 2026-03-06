using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IAchievementRepository : IBaseRepository<Achievement>
    {
        Task<Achievement?> GetByIdAsync(Guid achievementId);
        Task<List<Achievement>> GetByPlayersSlaveIdAsync(Guid playersSlaveId);
    }
}
