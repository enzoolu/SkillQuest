namespace SkillQuest.Api.DTOs
{
    public class ProgressoDto
    {
        public int IdUsuario { get; set; }
        public int IdMissao { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime? DataConclusao { get; set; }
        public int PontosGanhos { get; set; }
        public int PontosTotaisUsuario { get; set; }
    }
}