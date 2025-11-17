using SkillQuest.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkillQuest.Api.Repositories
{
    public interface IMissaoRepository
    {
        Task<Missao?> GetByIdAsync(int id);
        Task<IEnumerable<Missao>> GetMissoesByTrilhaIdAsync(int idTrilha);
        Task<Missao> AddAsync(Missao missao);
        Task UpdateAsync(Missao missao);
        Task DeleteAsync(Missao missao);
    }
}