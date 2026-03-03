using Gladiators.Business.Resources;
using Gladiators.Data.Entities;
using Gladiators.Data.Enums;

public static class AchievementTextHelper
{
    public static string GetTooltipText(Achievement achievement)
    {
        int level = Math.Min(achievement.Level, 5);

        return achievement.Type switch
        {
            AchievementType.Veteran =>
                string.Format(AchievementDescriptions.Veteran, level, level * 5),

            AchievementType.CriticalMaster =>
                string.Format(AchievementDescriptions.CriticalMaster, level, level * 3),

            AchievementType.PatientStriker =>
                string.Format(AchievementDescriptions.PatientStriker, level, level * 2),

            AchievementType.LastSurvivor =>
                string.Format(AchievementDescriptions.LastSurvivor, level, level * 10),

            AchievementType.Dominator =>
                string.Format(AchievementDescriptions.Dominator, level, level * 5, level * 5),

            _ => string.Empty
        };
    }
}