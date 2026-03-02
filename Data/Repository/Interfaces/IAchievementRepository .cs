using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IAchievementRepository : IBaseRepository<Achievement>
    {
        Task<List<Achievement>> GetByPlayersSlaveIdAsync(Guid playersSlaveId);
    }
}
