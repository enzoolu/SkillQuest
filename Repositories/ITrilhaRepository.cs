using SkillQuest.Api.Models;

namespace SkillQuest.Api.Repositories
{
    public interface ITrilhaRepository
    {
        Task<Trilha?> GetByIdAsync(int id);
        Task<IEnumerable<Trilha>> GetAllAsync();
        Task<Trilha> AddAsync(Trilha trilha);
        Task UpdateAsync(Trilha trilha);
        Task DeleteAsync(int id);
    }
}