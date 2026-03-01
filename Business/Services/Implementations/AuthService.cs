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

        public async Task<Guid> RegisterAsync(string username, string password)
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
            return user.Id;
        }

        public async Task<Guid?> LoginAsync(string username, string password)
        {
            var user = await _userRepo.GetUserByUsernameAsync(username);
            if (user == null || user.Password != password)
                return null;

            return user!.Id;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _userRepo.GetAllAsync();
        }

        public async Task<int> DeleteUserAsync(Guid id)
        {
            var user = await _userRepo.GetAsync(id); // ищем пользователя по Id
            if (user == null)
                return 0; // не найден

            return await _userRepo.DeleteAsync(user); // возвращает количество удалённых строк (1 если успешно)
        }
    }
}
