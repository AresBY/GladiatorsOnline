using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gladiators.Data.Repository.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public BaseRepository(AppDbContext context) => _context = context;

        public Task<List<T>> GetAllAsync() => _context.Set<T>().ToListAsync();
        public Task<T?> GetByIdAsync(Guid id) => _context.Set<T>().FindAsync(id).AsTask();
        public Task AddAsync(T entity) { _context.Set<T>().Add(entity); return _context.SaveChangesAsync(); }
        public Task UpdateAsync(T entity) { _context.Set<T>().Update(entity); return _context.SaveChangesAsync(); }
        public Task DeleteAsync(T entity) { _context.Set<T>().Remove(entity); return _context.SaveChangesAsync(); }
    }
}
