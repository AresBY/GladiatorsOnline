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

        /// <summary>
        /// Увеличить Intuition раба на 10
        /// </summary>
        [HttpPost("{playerSlaveId}/add-ten-intuition")]
        public async Task<IActionResult> AddTenIntuition(Guid playerSlaveId)
        {
            if (playerSlaveId == Guid.Empty)
                return BadRequest("playerSlaveId не может быть пустым");

            var slave = await _playerSlaveService.GetAsync(playerSlaveId);
            if (slave == null)
                return NotFound($"Раб с Id {playerSlaveId} не найден");

            // Увеличиваем Intuition на 10
            slave.Intuition += 10;

            // Сохраняем изменения
            await _playerSlaveService.UpdateAsync(slave);

            //await UpdateStatsAchivsAsync

            return Ok(new { slaveId = playerSlaveId, newIntuition = slave.Intuition });
        }
    }
}