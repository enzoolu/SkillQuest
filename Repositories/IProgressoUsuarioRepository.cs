using SkillQuest.Api.Models;

namespace SkillQuest.Api.Repositories
{
    public interface IProgressoUsuarioRepository
    {
        Task<ProgressoUsuario?> GetByIdAsync(int idUsuario, int idMissao);
        Task<ProgressoUsuario> AddAsync(ProgressoUsuario progresso);
        Task<ProgressoUsuario> UpdateAsync(ProgressoUsuario progresso);
        Task<IEnumerable<ProgressoUsuario>> GetProgressosByUsuarioAsync(int idUsuario);
    }
}