using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IAchievementService
    {
        Task<List<Achievement>> AwardAchivesIfNeededAsync(Battle battle);
        Task UpdateStatsAchivsAsync(Guid playersSlaveId);
        Task<List<Achievement>> GetAsync(Guid playersSlave);
        Task AddAsync(Achievement achievement);
        Task UpdateAsync(Achievement achievement);
        Task DeleteAsync(Guid achievementId);
    }
}
