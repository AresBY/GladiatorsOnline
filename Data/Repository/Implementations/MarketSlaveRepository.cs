using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data.Repository.Implementations
{
    public class MarketSlaveRepository : BaseRepository<MarketSlave>, IMarketSlaveRepository
    {
        private readonly AppDbContext _context;

        public MarketSlaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public Task<List<MarketSlave>> GetAllAsync(Guid playerId) =>
            _context.MarketSlaves.Where(t => t.PlayerId == playerId).ToListAsync();

        public Task<MarketSlave?> GetAsync(Guid slaveId) =>
            _context.MarketSlaves.FirstOrDefaultAsync(s => s.Id == slaveId);

        public async Task UpdateMarketSlavesAsync(List<MarketSlave> newSlaves)
        {
            await _context.MarketSlaves.AddRangeAsync(newSlaves);
            await _context.SaveChangesAsync();
        }
    }
}