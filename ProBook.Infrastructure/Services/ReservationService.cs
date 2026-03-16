using System.Linq;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;
using ProBook.Domain.Entities;
using ProBook.Domain.Enums;

namespace ProBook.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        private readonly List<Reservation> _reservations = new List<Reservation>();
        private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Name = "Admin", Email = "admin@probook.com", Role = UserRole.Manager, HasReserved = false }
        };

        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            return _reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                UserId = r.UserId,
                RoomId = r.RoomId,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Status = r.Status,
                TotalPrice = r.TotalPrice
            });
        }

        public async Task<IEnumerable<ReservationDto>> GetReservationsByUserIdAsync(int userId)
        {
            return _reservations.Where(r => r.UserId == userId).Select(r => new ReservationDto
            {
                Id = r.Id,
                UserId = r.UserId,
                RoomId = r.RoomId,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                Status = r.Status,
                TotalPrice = r.TotalPrice
            });
        }

        public async Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto)
        {
            var user = _users.FirstOrDefault(u => u.Id == reservationDto.UserId);
            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            if (user.HasReserved)
            {
                throw new InvalidOperationException("User has already made a reservation");
            }

            var newReservation = new Reservation
            {
                Id = _reservations.Any() ? _reservations.Max(r => r.Id) + 1 : 1,
                UserId = reservationDto.UserId,
                RoomId = reservationDto.RoomId,
                CheckIn = reservationDto.CheckIn,
                CheckOut = reservationDto.CheckOut,
                Status = ReservationStatus.Confirmed,
                TotalPrice = reservationDto.TotalPrice
            };

            _reservations.Add(newReservation);
            user.HasReserved = true;

            return new ReservationDto
            {
                Id = newReservation.Id,
                UserId = newReservation.UserId,
                RoomId = newReservation.RoomId,
                CheckIn = newReservation.CheckIn,
                CheckOut = newReservation.CheckOut,
                Status = newReservation.Status,
                TotalPrice = newReservation.TotalPrice
            };
        }
    }
}