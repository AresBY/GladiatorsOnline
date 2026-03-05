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
            // Определяем победителя
            var winner = battle.FirstFighter.IsWinner ? battle.FirstFighter : battle.SecondFighter;

            int remainingHP = winner.MaxHP;

            int critStreak = 0;
            int dodgeStreak = 0;
            int missStreak = 0;
            int critStreakOnMe = 0;
            bool twoCritsInRow = false;
            bool twoCritsInRowOnMe = false;
            bool fourDodgeInRow = false;
            bool anyCrit = false;
            bool threeMissInRow = false;

            var rounds = battle.BattleRounds;

            for (int i = 0; i < rounds.Count; i++)
            {
                // Чередование ходов: 0-й ход — FirstFighter, 1-й — SecondFighter и т.д.
                bool isWinnerTurn = (i % 2 == 0 && battle.FirstFighter.IsWinner) ||
                                    (i % 2 == 1 && battle.SecondFighter.IsWinner);
                bool isWinnerTarget = !isWinnerTurn;

                var round = rounds[i];

                if (isWinnerTurn)
                {
                    if (!twoCritsInRow)
                    {
                        if (round.Critical)
                        {
                            critStreak++;
                            anyCrit = true;

                            if (critStreak > 1)
                                twoCritsInRow = true;
                        }
                        else
                        {
                            critStreak = 0;
                        }
                    }

                    if (!threeMissInRow)
                    {
                        if (round.Missed)
                        {
                            missStreak++;
                            if (missStreak > 2)
                                threeMissInRow = true;
                        }
                        else
                        {
                            missStreak = 0;
                        }
                    }
                }

                if (!isWinnerTurn)
                {
                    if (!fourDodgeInRow)
                    {
                        if (round.Missed)
                        {
                            dodgeStreak++;


                            if (dodgeStreak > 3)
                                fourDodgeInRow = true;
                        }
                        else
                        {
                            dodgeStreak = 0;
                        }
                    }
                    if (!twoCritsInRowOnMe)
                    {
                        if (round.Critical)
                        {
                            critStreakOnMe++;

                            if (critStreakOnMe > 1)
                                twoCritsInRowOnMe = true;
                        }
                        else
                        {
                            critStreakOnMe = 0;
                        }
                    }
                }

                // Вычитаем урон, если целью был победитель
                if (isWinnerTarget)
                    remainingHP -= round.DamageDealt;
            }

            remainingHP = Math.Max(remainingHP, 0);

            bool lastSurvivor = remainingHP > 0 && remainingHP < winner.MaxHP * 0.2;
            bool dominator = remainingHP > winner.MaxHP * 0.8;
            bool patientStriker = !anyCrit;
            bool criticalMaster = twoCritsInRow;
            bool dodgeMaster = fourDodgeInRow;
            bool brokenFocus = threeMissInRow;
            bool critBreaker = twoCritsInRowOnMe;

            var existingAchievements =
                await _achievementRepo.GetByPlayersSlaveIdAsync(winner.Id);

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
                        PlayerSlaveId = winner.Id,
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
            await ProcessAchievement(AchievementType.DodgeMaster, dodgeMaster);
            await ProcessAchievement(AchievementType.BrokenFocus, brokenFocus);
            await ProcessAchievement(AchievementType.CritBreaker, critBreaker);

            return updatedAchievements;
        }
    }
}
