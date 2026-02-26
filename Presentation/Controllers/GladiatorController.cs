using Gladiators.Business.DTOs;
using Gladiators.Business.Mapping;
using Gladiators.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Gladiators.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GladiatorController : ControllerBase
    {
        private readonly IGladiatorService _service;

        public GladiatorController(IGladiatorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var gladiators = await _service.GetAllAsync();
            var result = gladiators.Select(g => g.ToDto()).ToList();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var gladiator = await _service.GetByIdAsync(id);
            if (gladiator == null) return NotFound();

            return Ok(gladiator.ToDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GladiatorDto gladiatorDto)
        {
            var gladiator = gladiatorDto.ToEntity();
            gladiator.Id = Guid.NewGuid();

            await _service.AddAsync(gladiator);
            return CreatedAtAction(nameof(GetById), new { id = gladiator.Id }, gladiator.ToDto());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GladiatorDto updatedDto)
        {
            var gladiator = await _service.GetByIdAsync(id);
            if (gladiator == null) return NotFound();

            gladiator.UpdateFromDto(updatedDto);

            await _service.UpdateAsync(gladiator);

            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var gladiator = await _service.GetByIdAsync(id);
            if (gladiator == null) return NotFound();

            await _service.DeleteAsync(gladiator);
            return NoContent();
        }
    }
}