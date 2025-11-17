using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.Models;

namespace SkillQuest.Api.Repositories
{
    public class TrilhaRepository : ITrilhaRepository
    {
        private readonly SkillQuestDbContext _context;
        public TrilhaRepository(SkillQuestDbContext context) { _context = context; }

        public async Task<Trilha> AddAsync(Trilha trilha)
        {
            await _context.Trilhas.AddAsync(trilha);
            await _context.SaveChangesAsync();
            return trilha;
        }

        public async Task DeleteAsync(int id)
        {
            var trilha = await GetByIdAsync(id);
            if (trilha != null)
            {
                _context.Trilhas.Remove(trilha);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Trilha>> GetAllAsync()
        {
            return await _context.Trilhas.AsNoTracking().ToListAsync();
        }

        public async Task<Trilha?> GetByIdAsync(int id)
        {
            return await _context.Trilhas.FindAsync(id);
        }

        public async Task UpdateAsync(Trilha trilha)
        {
            _context.Trilhas.Update(trilha);
            await _context.SaveChangesAsync();
        }
    }
}