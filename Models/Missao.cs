using System.Collections.Generic;

namespace SkillQuest.Api.Models
{
    public class Missao
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public int XP { get; set; }

        public int IdTrilha { get; set; }

        public Trilha Trilha { get; set; } = null!;
        public ICollection<ProgressoUsuario> Progressos { get; set; } = new List<ProgressoUsuario>();
    }
}