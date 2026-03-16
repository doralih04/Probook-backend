using ProBook.Domain.Enums;

namespace ProBook.Application.DTOs
{
    public class RoomDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}