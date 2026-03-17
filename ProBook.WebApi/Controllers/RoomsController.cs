using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar las habitaciones del hotel ProBook.
    /// Permite obtener, consultar y crear habitaciones.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        /// <summary>
        /// Constructor que inyecta el servicio de habitaciones.
        /// </summary>
        /// <param name="roomService">Servicio de habitaciones implementado en Infrastructure.</param>
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        /// <summary>
        /// Obtiene la lista completa de habitaciones disponibles.
        /// Incluye detalles como nombre, tipo, precio, descripción e imagen.
        /// </summary>
        /// <returns>Lista de habitaciones en formato DTO.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllRooms()
        {
            var rooms = await _roomService.GetAllRoomsAsync();
            return Ok(rooms);
        }

        /// <summary>
        /// Obtiene los detalles de una habitación específica por su ID.
        /// </summary>
        /// <param name="id">Identificador único de la habitación.</param>
        /// <returns>Detalles de la habitación o 404 si no existe.</returns>
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

        /// <summary>
        /// Crea una nueva habitación en el sistema.
        /// Requiere autenticación y rol de Manager.
        /// </summary>
        /// <param name="roomDto">Datos de la nueva habitación.</param>
        /// <returns>Habitación creada con su ID asignado.</returns>
        [HttpPost]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> CreateRoom([FromBody] RoomDto roomDto)
        {
            var createdRoom = await _roomService.CreateRoomAsync(roomDto);
            return CreatedAtAction(nameof(GetRoomById), new { id = createdRoom.Id }, createdRoom);
        }
    }
}