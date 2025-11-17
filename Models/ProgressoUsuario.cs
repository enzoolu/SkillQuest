namespace SkillQuest.Api.Models
{
    public class ProgressoUsuario
    {
        public int IdUsuario { get; set; }
        public int IdMissao { get; set; }

        public string Status { get; set; } = string.Empty;
        public DateTime? DataConclusao { get; set; }

        public Usuario Usuario { get; set; } = null!;
        public Missao Missao { get; set; } = null!;
    }
}