using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(Guid id);
        Task<int> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}