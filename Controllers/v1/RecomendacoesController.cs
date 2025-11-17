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
    [Route("api/v{version:apiVersion}/recomendacoes")]
    [Authorize(Roles = "Aluno")]
    public class RecomendacoesController : ControllerBase
    {
        private readonly IRecomendacaoService _recomendacaoService;

        public RecomendacoesController(IRecomendacaoService recomendacaoService)
        {
            _recomendacaoService = recomendacaoService;
        }

        // GET /api/v1/recomendacoes/proxima-trilha
        [HttpGet("proxima-trilha")]
        [ProducesResponseType(typeof(RecomendacaoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProximaTrilha()
        {
            var idUsuarioClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (idUsuarioClaim == null) return Unauthorized();

            var idUsuario = int.Parse(idUsuarioClaim);

            var recomendacao = await _recomendacaoService.ObterRecomendacaoAsync(idUsuario);
            return Ok(recomendacao);
        }
    }
}