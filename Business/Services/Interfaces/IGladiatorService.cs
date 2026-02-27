using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IGladiatorService
    {
        Task<List<Gladiator>> GetAllAsync();
        Task<Gladiator?> GetByIdAsync(Guid id);
        Task AddAsync(Gladiator gladiator);
        Task UpdateAsync(Gladiator gladiator);
        Task<int> DeleteAsync(Gladiator gladiator);
    }
}
