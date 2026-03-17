using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    /// <summary>
    /// Controlador para gestionar las reservas de habitaciones en ProBook.
    /// Maneja la creación, consulta y validación de reservas con reglas de negocio.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        /// <summary>
        /// Constructor que inyecta el servicio de reservas.
        /// </summary>
        /// <param name="reservationService">Servicio de reservas implementado en Infrastructure.</param>
        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        /// <summary>
        /// Obtiene todas las reservas del sistema.
        /// Solo accesible para usuarios con rol Manager.
        /// </summary>
        /// <returns>Lista completa de reservas.</returns>
        [HttpGet]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        /// <summary>
        /// Obtiene las reservas de un usuario específico.
        /// </summary>
        /// <param name="userId">ID del usuario cuyas reservas se quieren consultar.</param>
        /// <returns>Lista de reservas del usuario.</returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            return Ok(reservations);
        }

        /// <summary>
        /// Crea una nueva reserva para un usuario.
        /// Valida que el usuario no tenga reservas previas y que exista.
        /// </summary>
        /// <param name="reservationDto">Datos de la reserva (usuario, habitación, fechas, precio).</param>
        /// <returns>Reserva creada o error 400 si ya tiene reserva.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
        {
            try
            {
                var createdReservation = await _reservationService.CreateReservationAsync(reservationDto);
                return CreatedAtAction(nameof(GetReservationsByUser), new { userId = createdReservation.UserId }, createdReservation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}