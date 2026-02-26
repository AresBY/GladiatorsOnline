using Gladiators.Business.DTOs;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Gladiators.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Gladiators.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerSlaveController : ControllerBase
    {
        private readonly IPlayerSlaveService _playerService;

        public PlayerSlaveController(IPlayerSlaveService playerService)
        {
            _playerService = playerService;
        }

        // Получить всех
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid playerId)
        {
            var slaves = await _playerService.GetAllAsync(playerId);
            var result = slaves.Select(s => s.ToDto<PlayersSlaveDto>()).ToList();
            return Ok(result);
        }

        // Получить одного
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var slave = await _playerService.GetAsync(id);
            if (slave == null)
                return NotFound();

            return Ok(slave.ToDto<PlayersSlaveDto>());
        }

        // Обновить
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlayersSlaveDto dto)
        {
            var slave = await _playerService.GetAsync(id);
            if (slave == null)
                return NotFound();

            await _playerService.UpdateAsync(dto.ToEntity<PlayersSlave>());

            return NoContent();
        }

        // Удалить
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var slave = await _playerService.GetAsync(id);
            if (slave == null)
                return NotFound();

            await _playerService.DeleteAsync(id);
            return NoContent();
        }
    }
}