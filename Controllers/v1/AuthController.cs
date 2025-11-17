using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SkillQuest.Api.DTOs;
using SkillQuest.Api.Services;

namespace SkillQuest.Api.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _userService;

        public AuthController(IUsuarioService userService)
        {
            _userService = userService;
        }

        // POST /api/v1/auth/register
        [HttpPost("register")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                var userDto = await _userService.RegisterUserAsync(registerDto);

                return CreatedAtAction(
                    nameof(UsuariosController.GetUserById),
                    new { controller = "Usuarios", id = userDto.Id, version = "1.0" },
                    userDto);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // POST /api/v1/auth/login
        [HttpPost("login")]
        [ProducesResponseType(typeof(TokenResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var tokenResponse = await _userService.AuthenticateAsync(loginDto);

            if (tokenResponse == null)
            {
                return Unauthorized();
            }

            return Ok(tokenResponse);
        }
    }
}