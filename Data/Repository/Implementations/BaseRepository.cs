using Gladiators.Data;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    public BaseRepository(AppDbContext context) => _context = context;

    public Task<List<T>> GetAllAsync() =>
        _context.Set<T>().ToListAsync();

    public Task<T?> GetAsync(Guid id) =>
        _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);

    public async Task<int> AddAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public Task<int> DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        return _context.SaveChangesAsync();
    }
}