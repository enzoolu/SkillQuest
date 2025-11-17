using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillQuest.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/trilhas/{idTrilha}/missoes")]
    [Authorize]
    public class MissoesController : ControllerBase
    {
        private readonly IMissaoService _missaoService;

        public MissoesController(IMissaoService missaoService)
        {
            _missaoService = missaoService;
        }

        // POST /api/v1/trilhas/{idTrilha}/missoes
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(MissaoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateMissao(int idTrilha, [FromBody] CreateMissaoDto dto)
        {
            try
            {
                var missaoDto = await _missaoService.CreateMissaoAsync(idTrilha, dto);
                return CreatedAtAction(nameof(GetMissaoById),
                    new { idTrilha = idTrilha, idMissao = missaoDto.Id, version = "1.0" },
                    missaoDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // GET /api/v1/trilhas/{idTrilha}/missoes
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<MissaoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMissoesDaTrilha(int idTrilha)
        {
            var missoes = await _missaoService.GetMissoesByTrilhaAsync(idTrilha);
            return Ok(missoes);
        }

        // GET /api/v1/trilhas/{idTrilha}/missoes/{idMissao}
        [HttpGet("{idMissao}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(MissaoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMissaoById(int idTrilha, int idMissao)
        {
            var missao = await _missaoService.GetMissaoByIdAsync(idTrilha, idMissao);
            if (missao == null) return NotFound();
            return Ok(missao);
        }

        // PUT /api/v1/trilhas/{idTrilha}/missoes/{idMissao}
        [HttpPut("{idMissao}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(MissaoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMissao(int idTrilha, int idMissao, [FromBody] CreateMissaoDto dto)
        {
            var missaoDto = await _missaoService.UpdateMissaoAsync(idTrilha, idMissao, dto);
            if (missaoDto == null) return NotFound();
            return Ok(missaoDto);
        }

        // DELETE /api/v1/trilhas/{idTrilha}/missoes/{idMissao}
        [HttpDelete("{idMissao}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMissao(int idTrilha, int idMissao)
        {
            var success = await _missaoService.DeleteMissaoAsync(idTrilha, idMissao);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}