using Gladiators.Business.DTOs;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class MarketSlaveService : IMarketSlaveService
    {
        private readonly IMarketSlaveRepository _marketRepo;
        private readonly IPlayerSlaveRepository _playerSlaveRepo;
        private readonly ISlaveGenerator _slaveGenerator;
        private readonly Random _rnd = new();

        public MarketSlaveService(IPlayerSlaveRepository playerSlaveRepo, IMarketSlaveRepository marketRepo, ISlaveGenerator slaveGenerator)
        {
            _playerSlaveRepo = playerSlaveRepo;
            _marketRepo = marketRepo;
            _slaveGenerator = slaveGenerator;
        }

        // Получить всех доступных рабов для игрока

        public async Task<IEnumerable<MarketSlave>> GetAllAsync(Guid playerId)
        {
            var marketSlaves = await _marketRepo.GetAllAsync(playerId);

            // Если рынок пустой, обновляем
            if (marketSlaves.Count == 0)
            {
                var slaves = _slaveGenerator.CreateSlaves(5, playerId);
                await _marketRepo.UpdateMarketSlavesAsync(slaves);
                return slaves;
            }

            return marketSlaves;
        }

        // Покупка раба
        public async Task BuyAsync(Guid marketSlaveId)
        {
            var marketSlave = await _marketRepo.GetAsync(marketSlaveId);

            if (marketSlave == null)
                throw new Exception("Slave is not available on the market");

            var playersSlave = marketSlave
                .ToDto<MarketSlaveDto>()
                .ToEntity<PlayersSlave>();

            playersSlave.OwnerId = marketSlave.PlayerId;

            await _marketRepo.DeleteAsync(marketSlave);

            await _playerSlaveRepo.AddAsync(playersSlave);
        }
    }
}