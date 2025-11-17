namespace SkillQuest.Api.DTOs
{
    public class MissaoDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public int XP { get; set; }
        public int IdTrilha { get; set; }
    }
}