using System.Linq;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;
using ProBook.Domain.Entities;
using ProBook.Domain.Enums;
using ProBook.Infrastructure.MockData;

namespace ProBook.Infrastructure.Services
{
    public class RoomService : IRoomService
    {
        public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
        {
            return MockDataStore.Rooms.Select(r => new RoomDto
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
            var room = MockDataStore.Rooms.FirstOrDefault(r => r.Id == id);
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
                Id = MockDataStore.Rooms.Any() ? MockDataStore.Rooms.Max(r => r.Id) + 1 : 1,
                Name = roomDto.Name,
                Type = roomDto.Type,
                Price = roomDto.Price,
                Description = roomDto.Description,
                ImageUrl = roomDto.ImageUrl
            };
            MockDataStore.Rooms.Add(newRoom);

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