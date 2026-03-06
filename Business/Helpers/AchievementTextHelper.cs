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
                string.Format(AchievementDescriptions.Veteran, level, level * 5, level * 5),

            AchievementType.CriticalMaster =>
                string.Format(AchievementDescriptions.CriticalMaster, level, level * 10, level * 5),

            AchievementType.PatientStriker =>
                string.Format(AchievementDescriptions.PatientStriker, level, level * 15, level * 15, level * 15),

            AchievementType.LastSurvivor =>
                string.Format(AchievementDescriptions.LastSurvivor, level, level * 10),

            AchievementType.Dominator =>
                string.Format(AchievementDescriptions.Dominator, level, level * 5, level * 5),

            AchievementType.DodgeMaster =>
                string.Format(AchievementDescriptions.DodgeMaster, level, level * 10, level * 5),

            AchievementType.BrokenFocus =>
                string.Format(AchievementDescriptions.BrokenFocus, level, level * 10),

            AchievementType.CritBreaker =>
                string.Format(AchievementDescriptions.CritBreaker, level, level * 5, level * 5),

            AchievementType.StrengthBonus =>
                string.Format(AchievementDescriptions.StrengthBonus, level, level * 15),

            AchievementType.DexterityBonus =>
                string.Format(AchievementDescriptions.DexterityBonus, level, level * 15, level * 15),

            AchievementType.IntuitionBonus =>
                string.Format(AchievementDescriptions.IntuitionBonus, level, level * 15, level * 15),

            AchievementType.StaminaBonus =>
                string.Format(AchievementDescriptions.StaminaBonus, level, level * 15),

            _ => string.Empty
        };
    }
}


