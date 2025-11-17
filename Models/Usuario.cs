using SkillQuest.Api.Models.ValueObjects;
using System.Collections.Generic;
using System;

namespace SkillQuest.Api.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public Email Email { get; set; } = null!;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Pontos { get; set; }
        public string Medalhas { get; set; } = string.Empty; 

        public ICollection<ProgressoUsuario> Progressos { get; set; } = new List<ProgressoUsuario>();
    }
}