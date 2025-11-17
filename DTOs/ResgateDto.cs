namespace SkillQuest.Api.DTOs
{
    public class ResgateDto
    {
        public string Mensagem { get; set; } = string.Empty;
        public int PontosRestantes { get; set; }
        public string RecompensaAdquirida { get; set; } = string.Empty;
    }
}