using System.ComponentModel.DataAnnotations;

namespace SkillQuest.Api.DTOs
{
    public class CreateRecompensaDto
    {
        [Required]
        public string Nome { get; set; } = string.Empty;
        [Required]
        public string Tipo { get; set; } = string.Empty;
        [Range(1, 100000)]
        public int PontosNecessarios { get; set; }
    }
}