using Gladiators.Data.Entities;

namespace Gladiators.Business.Providers
{
    public class AchievementConfigProvider : IAchievementConfigProvider
    {
        private readonly List<AchievementDefinition> _achievements;

        public AchievementConfigProvider(List<AchievementDefinition> achievements)
        {
            _achievements = achievements;
        }

        public IReadOnlyList<AchievementDefinition> GetAll() => _achievements;
    }
}
