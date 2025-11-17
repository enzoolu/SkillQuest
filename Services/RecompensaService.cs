using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public class RecompensaService : IRecompensaService
    {
        private readonly SkillQuestDbContext _context;
        private readonly IRecompensaRepository _recompensaRepo;

        public RecompensaService(SkillQuestDbContext context, IRecompensaRepository recompensaRepo)
        {
            _context = context;
            _recompensaRepo = recompensaRepo;
        }

        public async Task<RecompensaDto> CreateAsync(CreateRecompensaDto createDto)
        {
            var recompensa = new Recompensa
            {
                Nome = createDto.Nome,
                Tipo = createDto.Tipo,
                PontosNecessarios = createDto.PontosNecessarios
            };
            var novaRecompensa = await _recompensaRepo.AddAsync(recompensa);
            return MapToDto(novaRecompensa);
        }

        public async Task<IEnumerable<RecompensaDto>> GetAllAsync()
        {
            var recompensas = await _recompensaRepo.GetAllAsync();
            return recompensas.Select(MapToDto);
        }

        public async Task<RecompensaDto?> GetByIdAsync(int id)
        {
            var recompensa = await _recompensaRepo.GetByIdAsync(id);
            return (recompensa == null) ? null : MapToDto(recompensa);
        }

        public async Task<RecompensaDto?> UpdateAsync(int id, CreateRecompensaDto updateDto)
        {
            var recompensa = await _recompensaRepo.GetByIdAsync(id);
            if (recompensa == null) return null;

            recompensa.Nome = updateDto.Nome;
            recompensa.Tipo = updateDto.Tipo;
            recompensa.PontosNecessarios = updateDto.PontosNecessarios;

            _context.Recompensas.Update(recompensa);
            await _context.SaveChangesAsync();

            return MapToDto(recompensa);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var recompensa = await _recompensaRepo.GetByIdAsync(id);
            if (recompensa == null) return false;

            await _recompensaRepo.DeleteAsync(id);
            return true;
        }

        public async Task<ResgateDto> ResgatarRecompensaAsync(int idUsuario, int idRecompensa)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                var usuario = await _context.Usuarios.FindAsync(idUsuario);
                var recompensa = await _context.Recompensas.FindAsync(idRecompensa);

                if (usuario == null) throw new KeyNotFoundException("Usuário não encontrado.");
                if (recompensa == null) throw new KeyNotFoundException("Recompensa não encontrada.");

                if (usuario.Pontos < recompensa.PontosNecessarios)
                {
                    throw new InvalidOperationException("Pontos insuficientes.");
                }

                usuario.Pontos -= recompensa.PontosNecessarios;

                if (recompensa.Tipo == "Medalha")
                {
                    usuario.Medalhas += $";{recompensa.Nome}";
                }

                _context.Usuarios.Update(usuario);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new ResgateDto
                {
                    Mensagem = "Recompensa resgatada com sucesso!",
                    PontosRestantes = usuario.Pontos,
                    RecompensaAdquirida = recompensa.Nome
                };
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private RecompensaDto MapToDto(Recompensa r) => new RecompensaDto
        {
            Id = r.Id,
            Nome = r.Nome,
            Tipo = r.Tipo,
            PontosNecessarios = r.PontosNecessarios
        };
    }
}