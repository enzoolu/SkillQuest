using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Repositories;
using SkillQuest.Api.Models;
using SkillQuest.Api.Models.Enums;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SkillQuest.Api.Services
{
    public class RecomendacaoService : IRecomendacaoService
    {
        private readonly SkillQuestDbContext _context;
        public RecomendacaoService(SkillQuestDbContext context)
        {
            _context = context;
        }

        public async Task<RecomendacaoDto> ObterRecomendacaoAsync(int idUsuario)
        {
            var missoesCompletasIds = await _context.ProgressosUsuarios
                .Where(p => p.IdUsuario == idUsuario && p.Status == "Concluido")
                .Select(p => p.IdMissao)
                .ToListAsync();

            var trilhasCompletasIds = await _context.Missoes
                .Where(m => missoesCompletasIds.Contains(m.Id))
                .Select(m => m.IdTrilha)
                .Distinct()
                .ToListAsync();

            var trilhaRecomendada = await _context.Trilhas
                .Where(t => !trilhasCompletasIds.Contains(t.Id))
                .FirstOrDefaultAsync();

            if (trilhaRecomendada == null)
            {
                return new RecomendacaoDto { Mensagem = "Parabéns, completou tudo!", TrilhaRecomendada = null! };
            }

            if (trilhaRecomendada.Nivel == NivelTrilha.Iniciante)
            {
                var trilhaSoftSkills = await _context.Trilhas
                    .Where(t => t.Categoria == "Soft Skills" && !trilhasCompletasIds.Contains(t.Id))
                    .FirstOrDefaultAsync();

                if (trilhaSoftSkills != null)
                {
                    trilhaRecomendada = trilhaSoftSkills;
                }
            }

            return new RecomendacaoDto
            {
                Mensagem = "Recomendação baseada no seu nível!",
                TrilhaRecomendada = new TrilhaDto
                {
                    Id = trilhaRecomendada.Id,
                    Nome = trilhaRecomendada.Nome,
                    Descricao = trilhaRecomendada.Descricao,
                    Categoria = trilhaRecomendada.Categoria,
                    Nivel = trilhaRecomendada.Nivel.ToString()
                }
            };
        }
    }
}