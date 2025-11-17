using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuest.Api.Repositories
{
    public class ProgressoUsuarioRepository : IProgressoUsuarioRepository
    {
        private readonly SkillQuestDbContext _context;
        public ProgressoUsuarioRepository(SkillQuestDbContext context) { _context dotnet build= context; }

        public async Task<ProgressoUsuario> AddAsync(ProgressoUsuario progresso)
        {
            await _context.ProgressosUsuarios.AddAsync(progresso);
            return progresso;
        }

        public async Task<ProgressoUsuario?> GetByIdAsync(int idUsuario, int idMissao)
        {
            return await _context.ProgressosUsuarios
                .FindAsync(idUsuario, idMissao);
        }

        public async Task<IEnumerable<ProgressoUsuario>> GetProgressosByUsuarioAsync(int idUsuario)
        {
            return await _context.ProgressosUsuarios
                .Where(p => p.IdUsuario == idUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<ProgressoUsuario> UpdateAsync(ProgressoUsuario progresso)
        {
            _context.ProgressosUsuarios.Update(progresso);
            return Task.FromResult(progresso);
        }
    }
}