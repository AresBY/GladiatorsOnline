using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IAchievementService
    {
        Task<List<Achievement>> AwardLastSurvivorIfNeededAsync(Battle battle);
        Task<List<Achievement>> GetAchievementsAsync(Guid playersSlave);
    }
}
