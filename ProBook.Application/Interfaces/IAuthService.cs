using ProBook.Application.DTOs;
using ProBook.Application.DTOs.Auth;

namespace ProBook.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<UserDto> RegisterAsync(RegisterRequest request);
    }
}