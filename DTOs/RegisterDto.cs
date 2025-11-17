using System.ComponentModel.DataAnnotations;

namespace SkillQuest.Api.DTOs
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [RegularExpression("^(Admin|Aluno|EmpresaParceira)$", ErrorMessage = "O papel deve ser 'Admin', 'Aluno' ou 'EmpresaParceira'")]
        public string Role { get; set; } = string.Empty;
    }
}