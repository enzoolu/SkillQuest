namespace SkillQuest.Api.DTOs
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UsuarioDto User { get; set; } = null!;
    }
}