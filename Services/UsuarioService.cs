using Microsoft.EntityFrameworkCore;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Models;
using SkillQuest.Api.Repositories;

namespace SkillQuest.Api.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<TokenResponseDto?> AuthenticateAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return null;
            }

            var (token, expiresAt) = _tokenService.GenerateToken(user);

            return new TokenResponseDto
            {
                AccessToken = token,
                ExpiresAt = expiresAt,
                User = MapToDto(user)
            };
        }

        public async Task<UsuarioDto> RegisterUserAsync(RegisterDto registerDto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already in use.");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var user = new Usuario
            {
                Username = registerDto.Username,
                Email = new Models.ValueObjects.Email(registerDto.Email),
                PasswordHash = passwordHash,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow,
                Pontos = 0,
                Medalhas = ""
            };

            var newUser = await _userRepository.AddAsync(user);

            return MapToDto(newUser);
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToDto);
        }

        public async Task<UsuarioDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToDto(user);
        }

        private UsuarioDto MapToDto(Usuario user)
        {
            return new UsuarioDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email.Address,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                Pontos = user.Pontos,
                Medalhas = user.Medalhas
            };
        }
    }
}