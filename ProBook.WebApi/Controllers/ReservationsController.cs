using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;

namespace ProBook.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        [Authorize(Policy = "ManagerOnly")]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _reservationService.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetReservationsByUser(int userId)
        {
            var reservations = await _reservationService.GetReservationsByUserIdAsync(userId);
            return Ok(reservations);
        }

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