using Gladiators.Data.Entities;

namespace Gladiators.Business.Providers
{
    public interface IAchievementConfigProvider
    {
        IReadOnlyList<AchievementDefinition> GetAll();
    }
}
