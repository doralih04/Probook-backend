using ProBook.Application.DTOs;

namespace ProBook.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public UserDto User { get; set; }
    }
}