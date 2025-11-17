using SkillQuest.Api.DTOs;

namespace SkillQuest.Api.Services
{
    public interface IRecomendacaoService
    {
        Task<RecomendacaoDto> ObterRecomendacaoAsync(int idUsuario);
    }
}