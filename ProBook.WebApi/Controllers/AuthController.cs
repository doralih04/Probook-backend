using Microsoft.AspNetCore.Mvc;
using ProBook.Application.DTOs.Auth;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    /// <summary>
    /// Controlador para manejar la autenticación de usuarios en el sistema ProBook.
    /// Proporciona endpoints para login y registro de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor que inyecta el servicio de autenticación.
        /// </summary>
        /// <param name="authService">Servicio de autenticación implementado en Infrastructure.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Endpoint para autenticar a un usuario.
        /// Valida las credenciales y retorna un token JWT si son correctas.
        /// </summary>
        /// <param name="request">Objeto con email y contraseña del usuario.</param>
        /// <returns>Token JWT y datos del usuario autenticado.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request);
                return Ok(result);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }

        /// <summary>
        /// Endpoint para registrar un nuevo usuario en el sistema.
        /// Crea un usuario con rol 'Guest' y simula el registro exitoso.
        /// </summary>
        /// <param name="request">Datos del nuevo usuario (nombre, email, contraseña).</param>
        /// <returns>Datos del usuario registrado.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            return Ok(result);
        }
    }
}