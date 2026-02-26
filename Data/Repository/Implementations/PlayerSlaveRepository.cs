using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data.Repository.Implementations
{
    public class PlayerSlaveRepository : BaseRepository<PlayersSlave>, IPlayerSlaveRepository
    {
        private readonly AppDbContext _context;

        public PlayerSlaveRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Получить всех рабов игрока
        public async Task<List<PlayersSlave>> GetAllAsync(Guid playerId)
        {
            return await _context.PlayerSlaves
                .Where(s => s.OwnerId == playerId)
                .ToListAsync();
        }

        // Получить конкретного раба игрока
        public async Task<PlayersSlave?> GetAsync(Guid playerId, Guid slaveId)
        {
            return await _context.PlayerSlaves
                .FirstOrDefaultAsync(s => s.OwnerId == playerId && s.Id == slaveId);
        }
    }
}
