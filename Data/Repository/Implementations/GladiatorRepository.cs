using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Data.Repository.Implementations
{
    public class GladiatorRepository : BaseRepository<Fighter>, IGladiatorRepository
    {
        private readonly AppDbContext _context;

        public GladiatorRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
