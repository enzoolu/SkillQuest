using SkillQuest.Api.DTOs;
using System.Security.Claims;

namespace SkillQuest.Api.Services
{
    public interface IProgressoService
    {
        Task<ProgressoDto> CompletarMissaoAsync(int idUsuario, int idMissao);
    }
}