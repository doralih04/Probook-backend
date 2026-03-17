using System.Linq;
using ProBook.Application.DTOs;
using ProBook.Application.Interfaces;
using ProBook.Domain.Entities;
using ProBook.Domain.Enums;
using ProBook.Infrastructure.MockData;

namespace ProBook.Infrastructure.Services
{
    public class ReservationService : IReservationService
    {
        public async Task<IEnumerable<ReservationDto>> GetAllReservationsAsync()
        {
            return MockDataStore.Reservations.Select(r => new ReservationDto
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
            return MockDataStore.Reservations.Where(r => r.UserId == userId).Select(r => new ReservationDto
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
            var user = MockDataStore.Users.FirstOrDefault(u => u.Id == reservationDto.UserId);
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
                Id = MockDataStore.Reservations.Any() ? MockDataStore.Reservations.Max(r => r.Id) + 1 : 1,
                UserId = reservationDto.UserId,
                RoomId = reservationDto.RoomId,
                CheckIn = reservationDto.CheckIn,
                CheckOut = reservationDto.CheckOut,
                Status = ReservationStatus.Confirmed,
                TotalPrice = reservationDto.TotalPrice
            };

            MockDataStore.Reservations.Add(newReservation);
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