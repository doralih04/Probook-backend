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

namespace ProBook.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Admin", Email = "admin@probook.com", Role = UserRole.Manager, HasReserved = false }
        };

        private readonly string _secretKey = "your-secret-key-here"; // In production, use environment variable

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u => u.Email == request.Email);
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

        public async Task<UserDto> RegisterAsync(RegisterRequest request)
        {
            var newUser = new User
            {
                Id = _users.Max(u => u.Id) + 1,
                Name = request.Name,
                Email = request.Email,
                Role = UserRole.Guest,
                HasReserved = false
            };
            _users.Add(newUser);

            return new UserDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                Role = newUser.Role,
                HasReserved = newUser.HasReserved
            };
        }

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