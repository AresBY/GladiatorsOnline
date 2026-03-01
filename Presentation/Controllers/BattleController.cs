using Gladiators.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gladiators.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BattleController : ControllerBase
    {
        private readonly IBattleService _service;
        private readonly IPlayerSlaveService _playerSlaveService;

        public BattleController(IBattleService service, IPlayerSlaveService playerSlaveService)
        {
            _service = service;
            _playerSlaveService = playerSlaveService;
        }

        [HttpPost]
        public async Task<IActionResult> ExecuteBattle(Guid firstSlaveId, Guid secondSlaveId)
        {
            var result = await _service.ExecuteBattle(firstSlaveId, secondSlaveId);
            await _playerSlaveService.DeleteAsync(result.LoserId);
            var winner = await _playerSlaveService.GetAsync(result.WinnerId);
            winner.Wins += 1;
            await _playerSlaveService.UpdateAsync(winner);
            return Ok(result);
        }
    }
}
