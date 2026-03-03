using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Enums;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class AchievementService : IAchievementService
    {
        private readonly IAchievementRepository _achievementRepo;

        public AchievementService(IAchievementRepository achievementRepo)
        {
            _achievementRepo = achievementRepo;
        }

        public async Task<List<Achievement>> GetAchievementsAsync(Guid playersSlaveId)
        {
            return await _achievementRepo.GetByPlayersSlaveIdAsync(playersSlaveId);
        }

        public async Task<List<Achievement>> AwardLastSurvivorIfNeededAsync(Battle battle)
        {
            Guid winnerId = battle.WinnerId;
            bool isFirst = winnerId == battle.FirstSlaveID;

            int maxHP = isFirst ? battle.FirstSlaveMaxHP : battle.SecondSlaveMaxHP;
            int remainingHP = maxHP;

            int critStreak = 0;
            bool threeCritsInRow = false;
            bool anyCrit = false;

            var rounds = battle.BattleRounds;

            for (int i = 0; i < rounds.Count; i++)
            {
                bool isWinnerTurn = (i % 2 == 0 && isFirst) || (i % 2 == 1 && !isFirst);
                bool isWinnerTarget = !isWinnerTurn;

                var round = rounds[i];

                if (isWinnerTurn)
                {
                    if (round.Critical)
                    {
                        critStreak++;
                        anyCrit = true;

                        if (critStreak >= 3)
                            threeCritsInRow = true;
                    }
                    else
                    {
                        critStreak = 0;
                    }
                }

                if (isWinnerTarget)
                    remainingHP -= round.DamageDealt;
            }

            remainingHP = Math.Max(remainingHP, 0);

            bool lastSurvivor = remainingHP > 1 && remainingHP < maxHP * 0.1;
            bool dominator = remainingHP > maxHP * 0.8;
            bool patientStriker = !anyCrit;
            bool criticalMaster = threeCritsInRow;

            var existingAchievements =
                await _achievementRepo.GetByPlayersSlaveIdAsync(winnerId);

            var updatedAchievements = new List<Achievement>();

            async Task ProcessAchievement(AchievementType type, bool condition)
            {
                if (!condition)
                    return;

                var existing = existingAchievements
                    .FirstOrDefault(a => a.Type == type);

                if (existing != null)
                {
                    if (existing.Level < 5)
                    {
                        existing.Level++;
                        await _achievementRepo.UpdateAsync(existing);
                        updatedAchievements.Add(existing);
                    }
                }
                else
                {
                    var newAchievement = new Achievement
                    {
                        PlayerSlaveId = winnerId,
                        Type = type,
                        Level = 1
                    };

                    await _achievementRepo.AddAsync(newAchievement);
                    updatedAchievements.Add(newAchievement);
                }
            }

            await ProcessAchievement(AchievementType.Veteran, true);
            await ProcessAchievement(AchievementType.LastSurvivor, lastSurvivor);
            await ProcessAchievement(AchievementType.Dominator, dominator);
            await ProcessAchievement(AchievementType.PatientStriker, patientStriker);
            await ProcessAchievement(AchievementType.CriticalMaster, criticalMaster);

            return updatedAchievements;
        }
    }
    public class PlayerBattleStats
    {
        public Guid PlayerId { get; set; }
        public bool LowHP { get; set; }
        public bool ThreeCritsInRow { get; set; }
        public bool Won { get; set; }
        public bool NoCrits { get; set; }
        public bool Above80HP { get; set; }
    }
}
