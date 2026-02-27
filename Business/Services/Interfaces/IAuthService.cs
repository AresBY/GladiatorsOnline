using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Guid> RegisterAsync(string username, string password);
        Task<Guid?> LoginAsync(string username, string password);
        Task<List<User>> GetAllAsync();
        Task<int> DeleteUserAsync(Guid id);
    }
}