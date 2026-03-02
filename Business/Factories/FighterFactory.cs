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

            var achives = await _achievementService.GetAchievementsAsync(slave.Id);

            var fighter = new Fighter
            {
                Id = slave.Id,
                Name = slave.Name,
                Damage = slave.Strength * 2,
                HP = slave.Stamina * 20,
                HPMax = slave.Stamina * 20,

                Dodge = slave.Dexterity * 5,
                AntiDodge = (int)Math.Round(slave.Dexterity * 5 + slave.Intuition * 2.5),
                Critical = slave.Intuition * 5,
                AntiCritical = slave.Intuition * 5,

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
                        break;

                    case AchievementType.CriticalMaster:
                        // Увеличение шанса критического удара на 3% за уровень
                        fighter.Critical += 3 * level;
                        break;

                    case AchievementType.PatientStriker:
                        // Увеличение HP на 2% за уровень
                        fighter.HP += (int)Math.Round(fighter.HPMax * 0.02 * level);
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.02 * level);
                        break;

                    case AchievementType.LastSurvivor:
                        // Бонус к HP за каждый уровень
                        fighter.HP += (int)Math.Round(fighter.HPMax * 0.1 * level);
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.1 * level);
                        break;

                    case AchievementType.Dominator:
                        // Увеличение HP и Damage на 5% за уровень
                        fighter.HP += (int)Math.Round(fighter.HPMax * 0.05 * level);
                        fighter.HPMax += (int)Math.Round(fighter.HPMax * 0.05 * level);
                        fighter.Damage += (int)Math.Round(fighter.Damage * 0.05 * level);
                        break;

                        //case AchievementType.Unflinching:
                        //    // Увеличение уклонения на 2% за уровень
                        //    fighter.Dodge += 2 * level;
                        //    break;
                }
            }

            return fighter;
        }
    }
}
