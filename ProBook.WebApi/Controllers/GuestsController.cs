using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProBook.Infrastructure.MockData;
using ProBook.Domain.Entities;

namespace ProBook.WebApi.Controllers
{
    /// <summary>
    /// Controlador para visualizar la lista de huéspedes registrados y sus reservas.
    /// Solo accesible para usuarios con rol Manager.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "ManagerOnly")]
    public class GuestsController : ControllerBase
    {
        /// <summary>
        /// Obtiene todos los usuarios registrados con su estado de reserva.
        /// </summary>
        [HttpGet]
        public IActionResult GetGuests()
        {
            var guests = MockDataStore.Users.Select(u => new
            {
                id = u.Id,
                name = u.Name,
                email = u.Email,
                role = u.Role.ToString(),
                hasReserved = u.HasReserved,
                reservation = MockDataStore.Reservations.FirstOrDefault(r => r.UserId == u.Id) != null
                    ? new
                    {
                        roomId = MockDataStore.Reservations.First(r => r.UserId == u.Id).RoomId,
                        checkInDate = MockDataStore.Reservations.First(r => r.UserId == u.Id).CheckInDate,
                        checkOutDate = MockDataStore.Reservations.First(r => r.UserId == u.Id).CheckOutDate,
                        totalPrice = MockDataStore.Reservations.First(r => r.UserId == u.Id).TotalPrice
                    }
                    : null
            }).ToList();

            return Ok(guests);
        }
    }
}
