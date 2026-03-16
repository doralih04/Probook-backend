using ProBook.Application.DTOs;

namespace ProBook.Application.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservationsAsync();
        Task<IEnumerable<ReservationDto>> GetReservationsByUserIdAsync(int userId);
        Task<ReservationDto> CreateReservationAsync(ReservationDto reservationDto);
    }
}