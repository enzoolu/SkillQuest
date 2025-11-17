using System.ComponentModel.DataAnnotations;

namespace SkillQuest.Api.DTOs
{
    public class CreateTrilhaDto
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Descricao { get; set; } = string.Empty;

        [StringLength(100)]
        public string Categoria { get; set; } = string.Empty;

        [StringLength(50)]
        public string Nivel { get; set; } = string.Empty;
    }
}