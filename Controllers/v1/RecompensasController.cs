using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkillQuest.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/recompensas")]
    [Authorize]
    public class RecompensasController : ControllerBase
    {
        private readonly IRecompensaService _recompensaService;

        public RecompensasController(IRecompensaService recompensaService)
        {
            _recompensaService = recompensaService;
        }

        // GET /api/v1/recompensas
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<RecompensaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRecompensas()
        {
            var recompensas = await _recompensaService.GetAllAsync();
            return Ok(recompensas);
        }

        // GET /api/v1/recompensas/{id}
        [HttpGet("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RecompensaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetRecompensaById(int id)
        {
            var recompensa = await _recompensaService.GetByIdAsync(id);
            if (recompensa == null) return NotFound();
            return Ok(recompensa);
        }

        // POST /api/v1/recompensas
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(RecompensaDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateRecompensa([FromBody] CreateRecompensaDto createDto)
        {
            var recompensa = await _recompensaService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetRecompensaById), new { id = recompensa.Id, version = "1.0" }, recompensa);
        }

        // PUT /api/v1/recompensas/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(RecompensaDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRecompensa(int id, [FromBody] CreateRecompensaDto updateDto)
        {
            var recompensa = await _recompensaService.UpdateAsync(id, updateDto);
            if (recompensa == null) return NotFound();
            return Ok(recompensa);
        }

        // DELETE /api/v1/recompensas/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRecompensa(int id)
        {
            var success = await _recompensaService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }

        // POST /api/v1/recompensas/{idRecompensa}/resgatar
        [HttpPost("{idRecompensa}/resgatar")]
        [Authorize(Roles = UserRoles.Aluno)]
        [ProducesResponseType(typeof(ResgateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResgatarRecompensa(int idRecompensa)
        {
            var idUsuarioClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idUsuarioClaim == null) return Unauthorized();

            var idUsuario = int.Parse(idUsuarioClaim);

            try
            {
                var resgate = await _recompensaService.ResgatarRecompensaAsync(idUsuario, idRecompensa);
                return Ok(resgate);
            }
            catch (Exception ex) when (ex is KeyNotFoundException || ex is InvalidOperationException)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}