namespace SkillQuest.Api.Models
{
    public class Trilha
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string Nivel { get; set; } = string.Empty;

        public ICollection<Missao> Missoes { get; set; } = new List<Missao>();
    }
}