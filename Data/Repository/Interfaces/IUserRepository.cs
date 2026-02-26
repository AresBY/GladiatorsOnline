using Gladiators.Data.Entities;

namespace Gladiators.Data.Repository.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}