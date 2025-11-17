namespace SkillQuest.Api.DTOs
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public int Pontos { get; set; }
        public string Medalhas { get; set; } = string.Empty;
    }
}