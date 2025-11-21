using SkillQuest.Api.Models.Enums;
using System.Collections.Generic;

namespace SkillQuest.Api.Models
{
    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;

        public NivelTrilha Nivel { get; set; }

        public ICollection<Missao> Missoes { get; set; } = new List<Missao>();
    }
}