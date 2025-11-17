using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Services;
using System.Security.Claims;

namespace SkillQuest.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/progressos")]
    [Authorize(Roles = "Aluno")]
    public class ProgressosController : ControllerBase
    {
        private readonly IProgressoService _progressoService;

        public ProgressosController(IProgressoService progressoService)
        {
            _progressoService = progressoService;
        }

        // POST /api/v1/progressos/missoes/{idMissao}/completar
        [HttpPost("missoes/{idMissao}/completar")]
        [ProducesResponseType(typeof(ProgressoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CompletarMissao(int idMissao)
        {
            var idUsuarioClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idUsuarioClaim == null)
                return Unauthorized();

            var idUsuario = int.Parse(idUsuarioClaim);

            try
            {
                var progresso = await _progressoService.CompletarMissaoAsync(idUsuario, idMissao);
                return Ok(progresso);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}