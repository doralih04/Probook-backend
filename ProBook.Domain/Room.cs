using ProBook.Domain.Enums;

namespace ProBook.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public RoomType Type { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Amenities { get; set; } = new List<string>();
    }
}