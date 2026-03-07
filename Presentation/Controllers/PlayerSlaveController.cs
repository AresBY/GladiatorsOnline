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
        private readonly IPlayerSlaveService _playerSlaveService;

        public PlayerSlaveController(IPlayerSlaveService playerService)
        {
            _playerSlaveService = playerService;
        }

        // Получить всех
        [HttpGet]
        public async Task<IActionResult> GetAll(Guid playerId)
        {
            var result = await _playerSlaveService.GetAllAsync(playerId);
            return Ok(result);
        }

        // Получить одного
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var slave = await _playerSlaveService.GetAsync(id);
            if (slave == null)
                return NotFound();

            return Ok(slave.ToDto<PlayersSlaveDto>());
        }


        [HttpGet("{id:guid}/detail")]
        public async Task<IActionResult> GetDetailAsync(Guid id)
        {
            var slave = await _playerSlaveService.GetDetailAsync(id);
            if (slave == null)
                return NotFound();

            return Ok(slave);
        }

        // Обновить
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PlayersSlaveDto dto)
        {
            var slave = await _playerSlaveService.GetAsync(id);
            if (slave == null)
                return NotFound();

            await _playerSlaveService.UpdateAsync(dto.ToEntity<PlayersSlave>());

            return NoContent();
        }

        // Удалить
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var slave = await _playerSlaveService.GetAsync(id);
            if (slave == null)
                return NotFound();

            await _playerSlaveService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{playerSlaveId}/add-stat")]
        public async Task<IActionResult> AddStats(Guid playerSlaveId, [FromBody] AddStatRequest request)
        {
            if (playerSlaveId == Guid.Empty)
                return BadRequest("playerSlaveId не может быть пустым");

            if (request == null)
                return BadRequest("Request body не может быть пустым");

            const int amount = 10;

            await _playerSlaveService.AddStatsAsync(playerSlaveId, request.StatType, amount);

            return Ok(new
            {
                slaveId = playerSlaveId,
                statType = request.StatType
            });
        }
    }
}