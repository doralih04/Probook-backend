using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _roomService.GetRoomByIdAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            return Ok(room);
        }

        [HttpPost]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto roomDto)
        {
            var createdRoom = await _roomService.CreateRoomAsync(roomDto);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }
    }
}