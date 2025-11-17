using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.Models;

namespace SkillQuest.Api.Repositories
{
    public class RecompensaRepository : IRecompensaRepository
    {
        private readonly SkillQuestDbContext _context;
        public RecompensaRepository(SkillQuestDbContext context) { _context = context; }

        public async Task<Recompensa> AddAsync(Recompensa recompensa)
        {
            await _context.Recompensas.AddAsync(recompensa);
            await _context.SaveChangesAsync();
            return recompensa;
        }

        public async Task DeleteAsync(int id)
        {
            var recompensa = await GetByIdAsync(id);
            if (recompensa != null)
            {
                _context.Recompensas.Remove(recompensa);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Recompensa>> GetAllAsync()
        {
            return await _context.Recompensas.AsNoTracking().ToListAsync();
        }

        public async Task<Recompensa?> GetByIdAsync(int id)
        {
            return await _context.Recompensas.FindAsync(id);
        }
    }
}