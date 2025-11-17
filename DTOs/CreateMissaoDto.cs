using System.ComponentModel.DataAnnotations;

namespace SkillQuest.Api.DTOs
{
    public class CreateMissaoDto
    {
        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000)]
        public int XP { get; set; }
    }
}