using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IGladiatorService
    {
        Task<List<Fighter>> GetAllAsync();
        Task<Fighter?> GetByIdAsync(Guid id);
        Task AddAsync(Fighter gladiator);
        Task UpdateAsync(Fighter gladiator);
        Task<int> DeleteAsync(Fighter gladiator);
    }
}
