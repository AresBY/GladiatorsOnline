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
                .Include(s => s.Achievements)
                .ToListAsync();
        }
    }
}
