using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Gladiators.Data.Repository.Interfaces;

namespace Gladiators.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepo;

        public AuthService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task RegisterAsync(string username, string password)
        {
            var existingUser = await _userRepo.GetUserByUsernameAsync(username);
            if (existingUser != null)
                throw new Exception("Пользователь уже существует");

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                Password = password
            };

            await _userRepo.AddAsync(user);
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _userRepo.GetUserByUsernameAsync(username);
            if (user == null || user.Password != password)
                return null;

            return user;
        }
    }
}
