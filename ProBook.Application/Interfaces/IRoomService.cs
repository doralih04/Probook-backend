using ProBook.Application.DTOs;

namespace ProBook.Application.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
        Task<RoomDto> GetRoomByIdAsync(int id);
        Task<RoomDto> CreateRoomAsync(RoomDto roomDto);
    }
}