using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class GladiatorService : IGladiatorService
    {
        private readonly IGladiatorRepository _repository;

        public GladiatorService(IGladiatorRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Fighter>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Fighter?> GetByIdAsync(Guid id) => _repository.GetAsync(id);

        public Task AddAsync(Fighter gladiator) => _repository.AddAsync(gladiator);

        public Task UpdateAsync(Fighter gladiator) => _repository.UpdateAsync(gladiator);

        public Task<int> DeleteAsync(Fighter gladiator) => _repository.DeleteAsync(gladiator);
    }
}
