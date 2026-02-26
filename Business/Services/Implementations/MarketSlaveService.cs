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
        private readonly Random _rnd = new();

        public MarketSlaveService(IPlayerSlaveRepository playerSlaveRepo, IMarketSlaveRepository marketRepo)
        {
            _playerSlaveRepo = playerSlaveRepo;
            _marketRepo = marketRepo;
        }

        // Получить всех доступных рабов для игрока

        public async Task<IEnumerable<MarketSlave>> GetAllAsync(Guid playerId)
        {
            var market = await _marketRepo.GetAllAsync(playerId);

            // Если рынок пустой, обновляем
            if (market.Count == 0)
            {
                await UpdateMarketSlavesAsync(playerId);
                market = await _marketRepo.GetAllAsync(playerId);
            }

            return market;
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

        public async Task UpdateMarketSlavesAsync(Guid playerId)
        {
            var newMarketSlaves = new List<MarketSlave>();

            for (int i = 0; i < 5; i++) // генерируем 5 рабов
            {
                newMarketSlaves.Add(new MarketSlave
                {
                    Id = Guid.NewGuid(),
                    Name = $"Slave {_rnd.Next(1000, 9999)}",
                    Strength = _rnd.Next(1, 10),
                    Dexterity = _rnd.Next(1, 10),
                    Stamina = _rnd.Next(1, 10),
                    Price = _rnd.Next(50, 500),
                    PlayerId = playerId
                });
            }
            await _marketRepo.UpdateMarketSlavesAsync(newMarketSlaves);
        }
    }
}