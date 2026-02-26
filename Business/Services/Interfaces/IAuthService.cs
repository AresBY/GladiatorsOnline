using Gladiators.Data.Entities;

namespace Gladiators.Business.Services.Interfaces
{
    public interface IAuthService
    {
        Task RegisterAsync(string username, string password);
        Task<User?> LoginAsync(string username, string password);
    }
}