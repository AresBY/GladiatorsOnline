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
            var firstSlave = await _playerSlaveRepo.GetAsync(firstSlaveId);
            var secondSlave = await _playerSlaveRepo.GetAsync(secondSlaveId);

            if (firstSlave == null || secondSlave == null)
                throw new Exception("One or both gladiators not found");

            Fighter firstFighter = await _fighterFactory.Create(firstSlave);
            Fighter secondFighter = await _fighterFactory.Create(secondSlave);

            Battle battle = new Battle(firstSlave.Id, firstSlave.Name, firstFighter.HPMax,
                                      secondSlave.Id, secondSlave.Name, secondFighter.HPMax);


            int round = 0;

            while (firstFighter.HP > 0 && secondFighter.HP > 0)
            {
                Fighter attacker = round % 2 == 0 ? firstFighter : secondFighter;
                Fighter defender = round % 2 == 0 ? secondFighter : firstFighter;

                var attackResult = new AttackResult();

                float dodgeChance = 0f;
                if (defender.Dodge + attacker.AntiDodge > 0)
                    dodgeChance = (float)defender.Dodge / (defender.Dodge + attacker.AntiDodge);

                attackResult.Missed = _rnd.NextDouble() < dodgeChance;

                if (!attackResult.Missed)
                {
                    float critChance = 0f;
                    if (attacker.Critical + defender.AntiCritical > 0)
                        critChance = (float)attacker.Critical / (attacker.Critical + defender.AntiCritical);
                    attackResult.Critical = _rnd.NextDouble() < critChance;

                    attackResult.DamageDealt = attackResult.Critical ? attacker.Damage * 2 : attacker.Damage;

                    defender.HP -= attackResult.DamageDealt;
                }
                round++;

                battle.BattleRounds.Add(attackResult);
            }

            battle.WinnerId = firstFighter.HP > 0 ? firstFighter.Id : secondFighter.Id;
            battle.LoserId = firstFighter.HP <= 0 ? firstFighter.Id : secondFighter.Id;

            var achives = await _achievementService.AwardLastSurvivorIfNeededAsync(battle);
            return battle;
        }
    }
}
