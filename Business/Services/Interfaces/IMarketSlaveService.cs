using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IMarketSlaveService
    {
        /// <summary>
        /// Получить список доступных рабов для игрока.
        /// </summary>
        Task<IEnumerable<MarketSlave>> GetAllAsync(Guid playerId);

        /// <summary>
        /// Купить раба. После покупки он убирается с рынка.
        /// </summary>
        Task BuyAsync(Guid slaveId);

        /// <summary>
        /// Принудительно обновить рынок (сгенерировать новых рабов).
        /// </summary>
        Task UpdateMarketSlavesAsync(Guid playerId);

    }
}
