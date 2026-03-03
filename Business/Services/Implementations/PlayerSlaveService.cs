using Gladiators.Business.DTOs;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class PlayerSlaveService : IPlayerSlaveService
    {
        private readonly IPlayerSlaveRepository _playerSlaveRepo;

        public PlayerSlaveService(IPlayerSlaveRepository playerSlaveRepo)
        {
            _playerSlaveRepo = playerSlaveRepo;
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
            var player = await _playerSlaveRepo.GetAsync(id);
            if (player == null)
                throw new KeyNotFoundException($"Player with id {id} not found.");
            return player;
        }

        // Обновление
        public async Task UpdateAsync(PlayersSlave slave)
        {
            if (slave == null)
                throw new ArgumentNullException(nameof(slave));

            await _playerSlaveRepo.UpdateAsync(slave);
        }
    }
}