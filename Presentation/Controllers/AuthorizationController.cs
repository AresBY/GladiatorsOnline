using Gladiators.Business.DTOs;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gladiators.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCredentialsDto credentials)
        {
            try
            {
                var guid = await _authService.RegisterAsync(credentials.Username, credentials.Password);
                return Ok(guid);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Логин
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDto credentials)
        {
            var user = await _authService.LoginAsync(credentials.Username, credentials.Password);
            if (user == null)
                return Unauthorized("Неверный логин или пароль");

            return Ok(user);
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<User>>> GetAll()
        {
            var users = await _authService.GetAllAsync();
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _authService.DeleteUserAsync(id);
            return NoContent(); // 204 если удаление прошло
        }
    }
}