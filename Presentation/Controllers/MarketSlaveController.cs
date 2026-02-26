using Gladiators.Business.DTOs;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
namespace Gladiators.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarketSlaveController : ControllerBase
    {
        private readonly IMarketSlaveService _marketService;

        public MarketSlaveController(IMarketSlaveService marketService)
        {
            _marketService = marketService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] Guid playerId)
        {
            var slaves = await _marketService.GetAllAsync(playerId);
            var result = slaves.Select(g => g.ToDto<MarketSlaveDto>()).ToList();
            return Ok(result);
        }

        [HttpPost("buy/{slaveId:guid}")]
        public async Task<IActionResult> Buy(Guid slaveId, [FromQuery] Guid playerId)
        {
            if (playerId == Guid.Empty)
                return BadRequest(new { message = "PlayerId is required" });

            try
            {
                await _marketService.BuyAsync(slaveId);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Например, раб уже куплен
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}