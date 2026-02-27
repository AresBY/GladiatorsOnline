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

        public Task<List<Gladiator>> GetAllAsync() => _repository.GetAllAsync();

        public Task<Gladiator?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);

        public Task AddAsync(Gladiator gladiator) => _repository.AddAsync(gladiator);

        public Task UpdateAsync(Gladiator gladiator) => _repository.UpdateAsync(gladiator);

        public Task<int> DeleteAsync(Gladiator gladiator) => _repository.DeleteAsync(gladiator);
    }
}
