namespace SkillQuest.Api.DTOs
{
    public class TrilhaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;
    }
}