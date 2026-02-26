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
            var slave = await _playerSlaveRepo.GetByIdAsync(slaveId);
            if (slave == null)
            {
                throw new ArgumentNullException(nameof(slave));
            }
            await _playerSlaveRepo.DeleteAsync(slave);
        }

        // Получить всех рабов игрока
        public async Task<IEnumerable<PlayersSlave>> GetAllAsync(Guid playerId)
        {
            return await _playerSlaveRepo.GetAllAsync(playerId);
        }

        // Получить одного
        public async Task<PlayersSlave> GetAsync(Guid id)
        {
            return await _playerSlaveRepo.GetByIdAsync(id);
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