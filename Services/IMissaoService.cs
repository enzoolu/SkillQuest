using SkillQuest.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public interface IMissaoService
    {
        Task<MissaoDto> CreateMissaoAsync(int idTrilha, CreateMissaoDto dto);
        Task<MissaoDto?> UpdateMissaoAsync(int idTrilha, int idMissao, CreateMissaoDto dto);
        Task<bool> DeleteMissaoAsync(int idTrilha, int idMissao);
        Task<MissaoDto?> GetMissaoByIdAsync(int idTrilha, int idMissao);
        Task<IEnumerable<MissaoDto>> GetMissoesByTrilhaAsync(int idTrilha);
    }
}