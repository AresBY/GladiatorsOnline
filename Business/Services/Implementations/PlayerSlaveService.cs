using Gladiators.Business.DTOs;
using Gladiators.Business.Factories;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Enums;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class PlayerSlaveService : IPlayerSlaveService
    {
        private readonly IPlayerSlaveRepository _playerSlaveRepo;
        private readonly FighterFactory _fighterFactory;
        private readonly IAchievementService _achievementService;

        public PlayerSlaveService(IPlayerSlaveRepository playerSlaveRepo, FighterFactory fighterFactory,
            IAchievementService achievementService)
        {
            _playerSlaveRepo = playerSlaveRepo;
            _fighterFactory = fighterFactory;
            _achievementService = achievementService;
        }

        // Удаление
        public async Task DeleteAsync(Guid slaveId)
        {
            var slave = await _playerSlaveRepo.GetAsync(slaveId);
            if (slave == null)
            {
                throw new ArgumentNullException(nameof(slave));
            }
            await _playerSlaveRepo.DeleteAsync(slave);
        }

        // Получить всех рабов игрока
        public async Task<IEnumerable<PlayersSlaveDto>> GetAllAsync(Guid playerId)
        {
            var slaves = await _playerSlaveRepo.GetAllAsync(playerId);
            var result = slaves.Select(s => s.ToDto<PlayersSlaveDto>()).ToList();
            return result;
        }

        // Получить одного
        public async Task<PlayersSlave> GetAsync(Guid id)
        {
            var slave = await _playerSlaveRepo.GetAsync(id);
            if (slave == null)
                throw new KeyNotFoundException($"Player with id {id} not found.");
            return slave;
        }

        public async Task<FighterDetailDto> GetDetailAsync(Guid id)
        {
            var slave = await _playerSlaveRepo.GetAsync(id);
            if (slave == null)
                throw new KeyNotFoundException($"PlayerSlave with id {id} not found.");

            var fighter = await _fighterFactory.Create(slave);

            return fighter.ToDetailDto(slave);
        }

        // Обновление
        public async Task UpdateAsync(PlayersSlave slave)
        {
            if (slave == null)
                throw new ArgumentNullException(nameof(slave));

            await _playerSlaveRepo.UpdateAsync(slave);
        }
        public async Task AddStatsAsync(Guid playerSlaveId, StatType statType, int amount)
        {
            var slave = await _playerSlaveRepo.GetAsync(playerSlaveId);

            if (slave == null)
                throw new Exception($"Раб с Id {playerSlaveId} не найден");

            switch (statType)
            {
                case StatType.Strength:
                    slave.Strength += amount;
                    break;

                case StatType.Dexterity:
                    slave.Dexterity += amount;
                    break;

                case StatType.Intuition:
                    slave.Intuition += amount;
                    break;

                case StatType.Stamina:
                    slave.Stamina += amount;
                    break;

                default:
                    throw new ArgumentException("Неизвестный тип стата");
            }

            await _playerSlaveRepo.UpdateAsync(slave);

            await _achievementService.UpdateStatsAchivsAsync(playerSlaveId);
        }
    }
}