
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data.Repository.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
