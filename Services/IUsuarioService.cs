using SkillQuest.Api.DTOs;

namespace SkillQuest.Api.Services
{
    public interface IUsuarioService
    {
        Task<UsuarioDto?> GetUserByIdAsync(int id);
        Task<IEnumerable<UsuarioDto>> GetAllUsersAsync();
        Task<UsuarioDto> RegisterUserAsync(RegisterDto registerDto);
        Task<TokenResponseDto?> AuthenticateAsync(LoginDto loginDto);
    }
}