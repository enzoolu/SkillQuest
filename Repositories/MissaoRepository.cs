using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuest.Api.Repositories
{
    public class MissaoRepository : IMissaoRepository
    {
        private readonly SkillQuestDbContext _context;

        public MissaoRepository(SkillQuestDbContext context)
        {
            _context = context;
        }

        public async Task<Missao?> GetByIdAsync(int id)
        {
            return await _context.Missoes.FindAsync(id);
        }

        public async Task<Missao> AddAsync(Missao missao)
        {
            await _context.Missoes.AddAsync(missao);
            await _context.SaveChangesAsync();
            return missao;
        }

        public async Task DeleteAsync(Missao missao)
        {
            _context.Missoes.Remove(missao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Missao>> GetMissoesByTrilhaIdAsync(int idTrilha)
        {
            return await _context.Missoes
                .Where(m => m.IdTrilha == idTrilha)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateAsync(Missao missao)
        {
            _context.Missoes.Update(missao);
            await _context.SaveChangesAsync();
        }
    }
}