using SkillQuest.Api.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public interface IRecompensaService
    {
        Task<IEnumerable<RecompensaDto>> GetAllAsync();
        Task<RecompensaDto?> GetByIdAsync(int id);
        Task<RecompensaDto> CreateAsync(CreateRecompensaDto createDto);
        Task<RecompensaDto?> UpdateAsync(int id, CreateRecompensaDto updateDto);
        Task<bool> DeleteAsync(int id);
        Task<ResgateDto> ResgatarRecompensaAsync(int idUsuario, int idRecompensa);
    }
}