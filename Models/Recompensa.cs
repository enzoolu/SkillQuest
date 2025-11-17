namespace SkillQuest.Api.Models
{
    public class Recompensa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int PontosNecessarios { get; set; }
    }
}