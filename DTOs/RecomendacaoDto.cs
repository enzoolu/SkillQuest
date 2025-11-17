namespace SkillQuest.Api.DTOs
{
    public class RecomendacaoDto
    {
        public string Mensagem { get; set; } = string.Empty;
        public TrilhaDto TrilhaRecomendada { get; set; } = null!;
    }
}