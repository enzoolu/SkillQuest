using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Services;

namespace SkillQuest.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/trilhas")]
    [Authorize]
    public class TrilhasController : ControllerBase
    {
        private readonly ITrilhaService _trilhaService;

        public TrilhasController(ITrilhaService trilhaService)
        {
            _trilhaService = trilhaService;
        }

        // GET /api/v1/trilhas
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<TrilhaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllTrilhas()
        {
            var trilhas = await _trilhaService.GetAllAsync();
            return Ok(trilhas);
        }

        // GET /api/v1/trilhas/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrilhaById(int id)
        {
            var trilha = await _trilhaService.GetByIdAsync(id);
            if (trilha == null) return NotFound();
            return Ok(trilha);
        }

        // POST /api/v1/trilhas
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)] 
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> CreateTrilha([FromBody] CreateTrilhaDto createDto)
        {
            var trilhaDto = await _trilhaService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetTrilhaById), new { id = trilhaDto.Id, version = "1.0" }, trilhaDto);
        }

        // PUT /api/v1/trilhas/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(TrilhaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTrilha(int id, [FromBody] CreateTrilhaDto updateDto)
        {
            var trilhaDto = await _trilhaService.UpdateAsync(id, updateDto);
            if (trilhaDto == null) return NotFound();
            return Ok(trilhaDto);
        }

        // DELETE /api/v1/trilhas/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTrilha(int id)
        {
            var success = await _trilhaService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}