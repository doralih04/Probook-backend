using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProBook.Application.DTOs;
using ProBook.Application.DTOs.Auth;
using ProBook.Application.Interfaces;
using ProBook.Domain.Entities;
using ProBook.Domain.Enums;
using ProBook.Infrastructure.MockData;

namespace ProBook.Infrastructure.Services
{
    /// <summary>
    /// Servicio de autenticación que maneja login y registro de usuarios.
    /// Utiliza datos mock y genera tokens JWT para sesiones.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly string _secretKey = "your-super-secret-key-that-must-be-at-least-256-bits-long"; // In production, use environment variable

        /// <summary>
        /// Autentica a un usuario con email y contraseña.
        /// Valida contra datos mock (admin@probook.com/admin123 para Manager).
        /// </summary>
        /// <param name="request">Credenciales del usuario.</param>
        /// <returns>Token JWT y datos del usuario si es válido.</returns>
        /// <exception cref="UnauthorizedAccessException">Si las credenciales son inválidas.</exception>
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = MockDataStore.Users.FirstOrDefault(u => u.Email == request.Email);
            if (user == null || request.Password != "admin123") // Simple check
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            var token = GenerateJwtToken(user);
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                HasReserved = user.HasReserved
            };

            return new AuthResponse { Token = token, User = userDto };
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// Asigna rol 'Guest' por defecto y simula registro exitoso.
        /// </summary>
        /// <param name="request">Datos del nuevo usuario.</param>
        /// <returns>Datos del usuario registrado.</returns>
        public async Task<UserDto> RegisterAsync(RegisterRequest request)
        {
            var newUser = new User
            {
                Id = MockDataStore.Users.Any() ? MockDataStore.Users.Max(u => u.Id) + 1 : 1,
                Name = request.Name,
                Email = request.Email,
                Role = UserRole.Guest,
                HasReserved = false
            };
            MockDataStore.Users.Add(newUser);

            return new UserDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Role = newUser.Role,
                HasReserved = newUser.HasReserved
            };
        }

        /// <summary>
        /// Genera un token JWT para el usuario autenticado.
        /// Incluye claims de ID, email y rol.
        /// </summary>
        /// <param name="user">Usuario para el cual generar el token.</param>
        /// <returns>Token JWT como string.</returns>
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ProBook",
                audience: "ProBook",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}