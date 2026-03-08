using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Enums;

namespace Gladiators.Business.Factories
{
    public class FighterFactory
    {
        private readonly IAchievementService _achievementService;
        public FighterFactory(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        public async Task<Fighter> Create(PlayersSlave slave)
        {
            if (slave == null)
                throw new ArgumentNullException(nameof(slave));

            await _achievementService.UpdateStatsAchivsAsync(slave.Id);

            var achives = await _achievementService.GetAsync(slave.Id);

            var fighter = new Fighter
            {
                Id = slave.Id,
                Name = slave.Name,
                Damage = (int)Math.Round(slave.Strength * 2f + slave.Weight * 0.2f),

                HP = slave.Stamina * 20,
                HPMax = slave.Stamina * 20,

                Dodge = (int)Math.Round(slave.Dexterity * 30f),
                AntiDodge = (int)Math.Round(slave.Dexterity * 12 + slave.Intuition * 8f),
                Critical = (int)Math.Round(slave.Intuition * 17f),
                CriticalPower = Math.Max(2, (int)Math.Round(slave.Intuition * 0.08f)),
                AntiCritical = (int)Math.Round(slave.Intuition * 40f),

                Wins = slave.Wins
            };

            foreach (var ach in achives)
            {
                int level = Math.Min(ach.Level, 5); // Ограничение до 5

                switch (ach.Type)
                {
                    case AchievementType.Veteran:
                        // Увеличение урона на 5% за уровень
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.05 * level);
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.05 * level);
                        break;

                    case AchievementType.CriticalMaster:
                        // За каждый уровень ачивки увеличиваем Critical на (5 * level)% от текущего значения
                        fighter.Critical += (int)Math.Round(fighter.Critical * 0.1 * level);
                        fighter.CriticalPower += (int)Math.Round(fighter.CriticalPower * 0.05 * level);
                        break;

                    case AchievementType.PatientStriker:
                        // Увеличение HP на 5% и урон на 10% за уровень
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.10 * level);
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.010 * level);
                        fighter.AntiCritical += (int)Math.Round(fighter.AntiCritical * 0.010 * level);
                        break;

                    case AchievementType.LastSurvivor:
                        // Бонус к HP за каждый уровень
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.1 * level);
                        break;

                    case AchievementType.Dominator:
                        // Увеличение HP и Damage на 5% за уровень
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.05 * level);
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.05 * level);
                        break;

                    case AchievementType.DodgeMaster:
                        // Увеличение уклонения на 10% и урон на 5% за уровень
                        fighter.Dodge += (int)Math.Round(fighter.Dodge * 0.1 * level);
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.05 * level);
                        break;
                    case AchievementType.BrokenFocus:
                        // Увеличение уклонения на 10% и урон на 5% за уровень
                        fighter.AntiDodge += (int)Math.Round(fighter.AntiDodge * 0.1 * level);
                        break;
                    case AchievementType.CritBreaker:
                        // Увеличение уклонения на 10% и урон на 5% за уровень
                        fighter.AntiDodge += (int)Math.Round(fighter.AntiCritical * 0.05 * level);
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.05 * level);
                        break;
                    case AchievementType.StrengthBonus:
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.15 * level);
                        break;
                    case AchievementType.DexterityBonus:
                        fighter.Dodge += (int)Math.Round(fighter.Dodge * 0.15 * level);
                        fighter.AntiDodge += (int)Math.Round(fighter.AntiDodge * 0.15 * level);
                        break;
                    case AchievementType.IntuitionBonus:
                        fighter.Critical += (int)Math.Round(fighter.Critical * 0.15 * level);
                        fighter.AntiCritical += (int)Math.Round(fighter.AntiCritical * 0.15 * level);
                        break;
                    case AchievementType.StaminaBonus:
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.15 * level);
                        break;
                }
            }
            return fighter;
        }
    }
}

