using System.Linq;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;
using ProBook.Domain.Entities;
using ProBook.Domain.Enums;

namespace ProBook.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        private readonly List<Room> _rooms = new List<Room>
        {
            new Room { Id = 1, Name = "Suite Deluxe", Type = RoomType.Suite, Price = 200, Description = "Luxury suite with ocean view", ImageUrl = "https://images.unsplash.com/photo-1631049307264-da0ec9d70304?w=400" },
            new Room { Id = 2, Name = "Double Room", Type = RoomType.Double, Price = 120, Description = "Comfortable room for two", ImageUrl = "https://images.unsplash.com/photo-1611892440504-42a792e24d32?w=400" },
            new Room { Id = 3, Name = "Individual Room", Type = RoomType.Individual, Price = 80, Description = "Cozy room for one", ImageUrl = "https://images.unsplash.com/photo-1590490360182-c33d57733427?w=400" },
            new Room { Id = 4, Name = "Suite Premium", Type = RoomType.Suite, Price = 250, Description = "Premium suite with balcony", ImageUrl = "https://images.unsplash.com/photo-1582719478250-c89cae4dc85b?w=400" },
            new Room { Id = 5, Name = "Double Superior", Type = RoomType.Double, Price = 150, Description = "Superior double room", ImageUrl = "https://images.unsplash.com/photo-1566665797739-1674de7a421a?w=400" },
            new Room { Id = 6, Name = "Individual Economy", Type = RoomType.Individual, Price = 60, Description = "Budget room for one", ImageUrl = "https://images.unsplash.com/photo-1598300042247-d088f8ab3a91?w=400" }
        };

        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            return _rooms.Select(r => new RoomDto
            {
                Id = r.Id,
                Name = r.Name,
                Type = r.Type,
                Price = r.Price,
                Description = r.Description,
                ImageUrl = r.ImageUrl
            });
        }

        public async Task<RoomDto> GetRoomByIdAsync(int id)
        {
            var room = _rooms.FirstOrDefault(r => r.Id == id);
            if (room == null) return null;

            return new RoomDto
            {
                Id = room.Id,
                Name = room.Name,
                Type = room.Type,
                Price = room.Price,
                Description = room.Description,
                ImageUrl = room.ImageUrl
            };
        }

        public async Task<RoomDto> CreateRoomAsync(RoomDto roomDto)
        {
            var newRoom = new Room
            {
                Id = _rooms.Max(r => r.Id) + 1,
                Name = roomDto.Name,
                Type = roomDto.Type,
                Price = roomDto.Price,
                Description = roomDto.Description,
                ImageUrl = roomDto.ImageUrl
            };
            _rooms.Add(newRoom);

            return new RoomDto
            {
                Id = newRoom.Id,
                Name = newRoom.Name,
                Type = newRoom.Type,
                Price = newRoom.Price,
                Description = newRoom.Description,
                ImageUrl = newRoom.ImageUrl
            };
        }
    }
}