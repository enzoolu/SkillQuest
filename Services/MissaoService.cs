using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public class MissaoService : IMissaoService
    {
        private readonly IMissaoRepository _missaoRepo;
        private readonly ITrilhaRepository _trilhaRepo;

        public MissaoService(IMissaoRepository missaoRepo, ITrilhaRepository trilhaRepo)
        {
            _missaoRepo = missaoRepo;
            _trilhaRepo = trilhaRepo;
        }

        public async Task<MissaoDto> CreateMissaoAsync(int idTrilha, CreateMissaoDto dto)
        {
            var trilha = await _trilhaRepo.GetByIdAsync(idTrilha);
            if (trilha == null)
            {
                throw new KeyNotFoundException("A Trilha especificada não existe.");
            }

            var missao = new Missao
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                XP = dto.XP,
                IdTrilha = idTrilha 
            };

            var novaMissao = await _missaoRepo.AddAsync(missao);
            return MapToDto(novaMissao);
        }

        public async Task<MissaoDto?> UpdateMissaoAsync(int idTrilha, int idMissao, CreateMissaoDto dto)
        {
            var missao = await _missaoRepo.GetByIdAsync(idMissao);

            if (missao == null || missao.IdTrilha != idTrilha)
            {
                return null;
            }

            missao.Nome = dto.Nome;
            missao.Descricao = dto.Descricao;
            missao.XP = dto.XP;

            await _missaoRepo.UpdateAsync(missao);
            return MapToDto(missao);
        }

        public async Task<bool> DeleteMissaoAsync(int idTrilha, int idMissao)
        {
            var missao = await _missaoRepo.GetByIdAsync(idMissao);
            if (missao == null || missao.IdTrilha != idTrilha)
            {
                return false;
            }

            await _missaoRepo.DeleteAsync(missao);
            return true;
        }

        public async Task<MissaoDto?> GetMissaoByIdAsync(int idTrilha, int idMissao)
        {
            var missao = await _missaoRepo.GetByIdAsync(idMissao);
            if (missao == null || missao.IdTrilha != idTrilha)
            {
                return null;
            }
            return MapToDto(missao);
        }

        public async Task<IEnumerable<MissaoDto>> GetMissoesByTrilhaAsync(int idTrilha)
        {
            var missoes = await _missaoRepo.GetMissoesByTrilhaIdAsync(idTrilha);
            return missoes.Select(MapToDto);
        }

        private MissaoDto MapToDto(Missao missao)
        {
            return new MissaoDto
            {
                Id = missao.Id,
                Nome = missao.Nome,
                Descricao = missao.Descricao,
                XP = missao.XP,
                IdTrilha = missao.IdTrilha
            };
        }
    }
}