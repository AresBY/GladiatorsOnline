using Gladiators.Business.Factories;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class BattleService : IBattleService
    {
        private readonly IPlayerSlaveRepository _playerSlaveRepo;
        private readonly IAchievementService _achievementService;
        private readonly FighterFactory _fighterFactory;
        private static readonly Random _rnd = new Random();

        public BattleService(IPlayerSlaveRepository playerSlaveRepo, FighterFactory fighterFactory, IAchievementService achievementService)
        {
            _playerSlaveRepo = playerSlaveRepo;
            _fighterFactory = fighterFactory;
            _achievementService = achievementService;
        }
        public async Task<Battle> ExecuteBattle(Guid firstSlaveId, Guid secondSlaveId)
        {
            var firstSlaveEntity = await _playerSlaveRepo.GetAsync(firstSlaveId);
            var secondSlaveEntity = await _playerSlaveRepo.GetAsync(secondSlaveId);

            if (firstSlaveEntity == null || secondSlaveEntity == null)
                throw new Exception("One or both gladiators not found");

            if (firstSlaveEntity.Weight > secondSlaveEntity.Weight)
            {
                var temp = firstSlaveEntity;
                firstSlaveEntity = secondSlaveEntity;
                secondSlaveEntity = temp;
            }

            var firstFighter = await _fighterFactory.Create(firstSlaveEntity);
            var secondFighter = await _fighterFactory.Create(secondSlaveEntity);

            var firstSlave = new FighterInformation
            {
                Id = firstSlaveEntity.Id,
                Name = firstSlaveEntity.Name,
                HP = firstFighter.HPMax,
                MaxHP = firstFighter.HPMax,
                IsWinner = false
            };

            var secondSlave = new FighterInformation
            {
                Id = secondSlaveEntity.Id,
                Name = secondSlaveEntity.Name,
                HP = secondFighter.HPMax,
                MaxHP = secondFighter.HPMax,
                IsWinner = false
            };

            var battle = new Battle(firstSlave, secondSlave);

            int round = 0;
            while (firstSlave.HP > 0 && secondSlave.HP > 0)
            {
                var attacker = round % 2 == 0 ? firstFighter : secondFighter;
                var defender = round % 2 == 0 ? secondFighter : firstFighter;

                var attackResult = new AttackResult();

                // --- Вычисление шанса промаха (уклон)
                float dodgeTotal = defender.Dodge + attacker.AntiDodge;
                float dodgeChance = dodgeTotal > 0 ? (float)defender.Dodge / dodgeTotal : 0.5f;
                attackResult.Missed = _rnd.NextDouble() < dodgeChance;

                if (!attackResult.Missed)
                {
                    // --- Вычисление шанса крита
                    float critTotal = attacker.Critical + defender.AntiCritical;
                    float critChance = critTotal > 0 ? (float)attacker.Critical / critTotal : 0.5f;
                    attackResult.Critical = _rnd.NextDouble() < critChance;

                    // --- Вычисление урона
                    attackResult.DamageDealt = attackResult.Critical ? attacker.Damage * attacker.CriticalPower : attacker.Damage;

                    // Применяем урон к нужному Slave
                    if (round % 2 == 0)
                        secondSlave.HP -= attackResult.DamageDealt;
                    else
                        firstSlave.HP -= attackResult.DamageDealt;
                }

                round++;
                battle.BattleRounds.Add(attackResult);
            }

            if (firstSlave.HP > 0)
                firstSlave.IsWinner = true;
            else
                secondSlave.IsWinner = true;

            var achives = await _achievementService.AwardAchivesIfNeededAsync(battle);
            return battle;
        }
    }
}
