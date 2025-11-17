using SkillQuest.Api.Models;

namespace SkillQuest.Api.Repositories
{
    public interface IRecompensaRepository
    {
        Task<Recompensa?> GetByIdAsync(int id);
        Task<IEnumerable<Recompensa>> GetAllAsync();
        Task<Recompensa> AddAsync(Recompensa recompensa);
        Task DeleteAsync(int id);
    }
}