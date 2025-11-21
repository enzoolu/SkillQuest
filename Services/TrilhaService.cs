using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Models.Enums;
using SkillQuest.Api.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillQuest.Api.Services
{
    public class TrilhaService : ITrilhaService
    {
        private readonly ITrilhaRepository _trilhaRepository;

        public TrilhaService(ITrilhaRepository trilhaRepository)
        {
            _trilhaRepository = trilhaRepository;
        }

        public async Task<TrilhaDto> CreateAsync(CreateTrilhaDto createDto)
        {
            if (!Enum.TryParse<NivelTrilha>(createDto.Nivel, true, out var nivelEnum))
            {
                nivelEnum = NivelTrilha.Iniciante;
            }

            var trilha = new Trilha
            {
                Nome = createDto.Nome,
                Descricao = createDto.Descricao,
                Categoria = createDto.Categoria,
                Nivel = nivelEnum
            };

            var newTrilha = await _trilhaRepository.AddAsync(trilha);
            return MapToDto(newTrilha);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return false;

            await _trilhaRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TrilhaDto>> GetAllAsync()
        {
            var trilhas = await _trilhaRepository.GetAllAsync();
            return trilhas.Select(MapToDto);
        }

        public async Task<TrilhaDto?> GetByIdAsync(int id)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            return trilha == null ? null : MapToDto(trilha);
        }

        public async Task<TrilhaDto?> UpdateAsync(int id, CreateTrilhaDto updateDto)
        {
            var trilha = await _trilhaRepository.GetByIdAsync(id);
            if (trilha == null) return null;

            trilha.Nome = updateDto.Nome;
            trilha.Descricao = updateDto.Descricao;
            trilha.Categoria = updateDto.Categoria;

            if (Enum.TryParse<NivelTrilha>(updateDto.Nivel, true, out var nivelEnum))
            {
                trilha.Nivel = nivelEnum;
            }

            await _trilhaRepository.UpdateAsync(trilha);
            return MapToDto(trilha);
        }

        private TrilhaDto MapToDto(Trilha trilha)
        {
            return new TrilhaDto
            {
                Id = trilha.Id,
                Nome = trilha.Nome,
                Descricao = trilha.Descricao,
                Categoria = trilha.Categoria,
                Nivel = trilha.Nivel.ToString()
            };
        }
    }
}