using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data.Repository.Implementations
{
    public class AchievementRepository : BaseRepository<Achievement>, IAchievementRepository
    {
        private readonly AppDbContext _context;

        public AchievementRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Achievement>> GetByPlayersSlaveIdAsync(Guid playersSlaveId)
        {
            return await _context.Achievements
                .Where(a => a.PlayerSlaveId == playersSlaveId)
                .ToListAsync();
        }
    }
}
