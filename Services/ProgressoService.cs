using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Repositories;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public class ProgressoService : IProgressoService
    {
        private readonly SkillQuestDbContext _context;

        public ProgressoService(SkillQuestDbContext context)
        {
            _context = context;
        }

        public async Task<ProgressoDto> CompletarMissaoAsync(int idUsuario, int idMissao)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. Encontrar a Missão (para saber o XP)
                var missao = await _context.Missoes.FindAsync(idMissao);
                if (missao == null)
                    throw new KeyNotFoundException("Missão não encontrada.");

                // 2. Encontrar o Usuário (para adicionar os pontos)
                var usuario = await _context.Usuarios.FindAsync(idUsuario);
                if (usuario == null)
                    throw new KeyNotFoundException("Usuário não encontrado.");

                // 3. Verificar se o progresso já existe
                var progresso = await _context.ProgressosUsuarios
                    .FindAsync(idUsuario, idMissao);

                if (progresso != null)
                {
                    if (progresso.Status == "Concluido")
                        throw new InvalidOperationException("Missão já concluída anteriormente.");

                    progresso.Status = "Concluido";
                    progresso.DataConclusao = DateTime.UtcNow;
                    _context.ProgressosUsuarios.Update(progresso);
                }
                else
                {
                    // 4. Se não existia, criar o novo registo de Progresso
                    progresso = new ProgressoUsuario
                    {
                        IdUsuario = idUsuario,
                        IdMissao = idMissao,
                        Status = "Concluido",
                        DataConclusao = DateTime.UtcNow
                    };
                    await _context.ProgressosUsuarios.AddAsync(progresso);
                }

                // 5. Atribuir os pontos (XP) ao usuário
                usuario.Pontos += missao.XP;
                _context.Usuarios.Update(usuario);

                // 6. Salvar TODAS as alterações (Progresso e Usuario) atomicamente
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                // 7. Retornar o DTO de resposta
                return new ProgressoDto
                {
                    IdUsuario = usuario.Id,
                    IdMissao = missao.Id,
                    Status = progresso.Status,
                    DataConclusao = progresso.DataConclusao,
                    PontosGanhos = missao.XP,
                    PontosTotaisUsuario = usuario.Pontos
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}