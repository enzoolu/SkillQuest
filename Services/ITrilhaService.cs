using SkillQuest.Api.DTOs;

namespace SkillQuest.Api.Services
{
    public interface ITrilhaService
    {
        Task<TrilhaDto?> GetByIdAsync(int id);
        Task<IEnumerable<TrilhaDto>> GetAllAsync();
        Task<TrilhaDto> CreateAsync(CreateTrilhaDto createDto);
        Task<TrilhaDto?> UpdateAsync(int id, CreateTrilhaDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}