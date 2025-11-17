using SkillQuest.Api.Models;

namespace SkillQuest.Api.Services
{
    public interface ITokenService
    {
        (string token, DateTime expiresAt) GenerateToken(Usuario usuario);
    }
}