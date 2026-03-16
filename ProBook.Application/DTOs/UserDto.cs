using ProBook.Domain.Enums;

namespace ProBook.Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
        public bool HasReserved { get; set; }
    }
}